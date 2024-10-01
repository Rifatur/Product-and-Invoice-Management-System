using BasicInvoiceApp.Application.DTOs.invoice;
using BasicInvoiceApp.Application.Helper;

namespace BasicInvoiceApp.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto> AddInvoiceAsync(CreateInvoiceDto invoiceDto);
        Task UpdateInvoiceAsync(InvoiceDto invoiceDto);
        Task DeleteInvoiceAsync(int id);
        Task<PaginatedList<InvoiceDto>> GetInvoicesByCustomerIdAsync(int customerId, int pageNumber, int pageSize, DateTime? startDate, DateTime? endDate);
    }
}
