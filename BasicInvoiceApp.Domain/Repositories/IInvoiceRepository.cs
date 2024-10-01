using BasicInvoiceApp.Domain.Entities;

namespace BasicInvoiceApp.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
        IQueryable<Invoice> Query();
    }
}
