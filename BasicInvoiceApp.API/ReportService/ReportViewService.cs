using AspNetCore.Reporting;
using BasicInvoiceApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

namespace BasicInvoiceApp.API.ReportService
{

    public interface IReportViewService
    {
        byte[] GenarateReportAsync(string reportName, string reportType);
    }
    public class ReportViewService : IReportViewService
    {
        private readonly InvoiceDbContext _context;
        public ReportViewService(InvoiceDbContext context)
        {
            _context = context;
        }
        public byte[] GenarateReportAsync(string reportName, string reportType)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("BasicInvoiceApp.API.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}Report\\{1}.rdlc", fileDirPath, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcFilePath);

            // prepare data for report

            var invoices = _context.Invoices
                                  .Include(i => i.Items)
                                  .ToList();


            report.AddDataSource("dsInvoice", invoices);


            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var result = report.Execute(GetRenderType(reportType), 1, parameters);

            return result.MainStream;
        }

        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    renderType = RenderType.Pdf;
                    break;
                case "XLS":
                    renderType = RenderType.Excel;
                    break;
                case "WORD":
                    renderType = RenderType.Word;
                    break;
            }

            return renderType;
        }



    }

}
