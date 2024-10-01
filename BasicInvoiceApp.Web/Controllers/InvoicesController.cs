using AspNetCore.Reporting;
using BasicInvoiceApp.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BasicInvoiceApp.Web.Controllers
{
    public class InvoicesController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly InvoiceReportService _reportService;
        public InvoicesController(IWebHostEnvironment webHostEnvironment, InvoiceReportService reportService)
        {
            _webHostEnvironment = webHostEnvironment;
            _reportService = reportService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoiceReport(int invoiceId)
        {
            return await GenerateReport(invoiceId);
        }

        public async Task<IActionResult> GenerateReport(int invoiceId)
        {

            var invoice = await _reportService.GetInvoiceDataAsync(invoiceId);

            string mimetype = "";
            int extension = 1;
            string mimeType = "application/pdf";

            var path = $"{this._webHostEnvironment.ContentRootPath}\\Report\\GetInvoiceReport.rdlc";// "BasicInvoiceApp.Web.Report.GetInvoiceReport.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "InvoiceId", invoice.Id.ToString() },
                { "CustomerName", invoice.Customer.Name },
                { "TotalPrice", invoice.TotalAmount.ToString("C") }
            };

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsInvoice", invoice.Items);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }
    }
}
