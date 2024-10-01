using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using BasicInvoiceApp.API.ReportService;
using BasicInvoiceApp.Application.DTOs.invoice;
using BasicInvoiceApp.Application.Helper;
using BasicInvoiceApp.Application.Interfaces;
using BasicInvoiceApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Mime;


namespace BasicInvoiceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IConfiguration _configuration;
        private readonly InvoiceDbContext _context;
        private readonly IReportViewService _reportViewService;

        public InvoicesController(IInvoiceService invoiceService, IConfiguration configuration, InvoiceDbContext context, IReportViewService reportViewService)
        {
            _invoiceService = invoiceService;
            _configuration = configuration;
            _context = context;
            _reportViewService = reportViewService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(int id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<ActionResult> AddInvoice([FromBody] CreateInvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                // Log or return detailed validation errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, errors });
            }

            var createdInvoice = await _invoiceService.AddInvoiceAsync(invoiceDto);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.Id }, createdInvoice);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            if (id != invoiceDto.Id)
            {
                return BadRequest();
            }
            await _invoiceService.UpdateInvoiceAsync(invoiceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoice(int id)
        {
            await _invoiceService.DeleteInvoiceAsync(id);
            return NoContent();
        }
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<PaginatedList<InvoiceDto>>> GetInvoicesByCustomerId(int customerId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var invoices = await _invoiceService.GetInvoicesByCustomerIdAsync(customerId, pageNumber, pageSize, startDate, endDate);
            return Ok(invoices);
        }

        [HttpGet("ReportByStoredProcedure/{invoiceid}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int invoiceid)
        {

            var invoiceItems = await GetInvoiceItemsFromStoredProcedure(invoiceid);

            if (invoiceItems == null || !invoiceItems.Any())
            {
                return null; // No data found
            }

            // Assume all items share the same invoice header data
            var firstItem = invoiceItems.First();
            var invoiceReport = new InvoiceDto
            {
                Id = firstItem.Id,
                TotalAmount = invoiceItems.Sum(i => i.Total),
                Items = invoiceItems
            };


            return Ok(invoiceReport);
        }

        private async Task<List<InvoiceItemDto>> GetInvoiceItemsFromStoredProcedure(int invoiceId)
        {
            var invoiceItems = new List<InvoiceItemDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetInvoiceReport";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@InvoiceId", invoiceId));

                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        invoiceItems.Add(new InvoiceItemDto
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            ProductId = result.GetInt32(result.GetOrdinal("ProductId")),
                            ProductName = result.GetString(result.GetOrdinal("ProductName")),
                            Quantity = result.GetInt32(result.GetOrdinal("Quantity")),
                            Price = result.GetDecimal(result.GetOrdinal("Price")),
                            Total = result.GetDecimal(result.GetOrdinal("Total")),

                        });
                    }
                }
            }

            return invoiceItems;
        }

        [HttpGet("ReportDownload")]
        public IActionResult Get()
        {
            string mimetype = "";
            int extension = 1;
            var _reportPath = @"Report\GetInvoiceReport.rdlc";

            LocalReport localReport = new LocalReport(_reportPath);

            // Dados
            System.Data.DataTable dt = new System.Data.DataTable();

            using (SqlConnection conn = new SqlConnection("InvoicesDBConnection"))
            {
                using (SqlCommand cmd = new SqlCommand("GetInvoiceReport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceId", 1); // Replace with your actual InvoiceId

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            localReport.AddDataSource("DataSet1", dt);

            // Parametros do relatório
            Dictionary<string, string> reportParams = new Dictionary<string, string>();

            if (reportParams != null && reportParams.Count > 0) // if you use parameter in report
            {
                List<ReportParameter> reportparameter = new List<ReportParameter>();
                foreach (var record in reportParams)
                {
                    //reportparameter.Add(new ReportParameter(record.Key, new string[] { record.Value }));
                }
            }


            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters: reportParams);
            byte[] file = result.MainStream;

            Stream stream = new MemoryStream(file);
            return File(stream, "application/pdf", "InvoiceReport.pdf");

        }

        [HttpGet("Download/{reportName}/{reportType}")]
        public ActionResult GetReportDownload(string reportName, string reportType)
        {
            var reportFileByteString = _reportViewService.GenarateReportAsync(reportName, reportType);
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportName, reportType));
        }
        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLS":
                    outputFileName = reportName + ".xls";
                    break;
                case "WORD":
                    outputFileName = reportName + ".doc";
                    break;
            }

            return outputFileName;
        }

    }
}
