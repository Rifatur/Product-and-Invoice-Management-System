namespace BasicInvoiceApp.Application.Interfaces
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, string reportType);
    }
}
