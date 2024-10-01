using BasicInvoiceApp.Domain.Entities;
using BasicInvoiceApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BasicInvoiceApp.Infrastructure.Persistence.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceRepository(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Invoice> Query()
        {
            return _context.Invoices.Include(i => i.Items);
        }
        public async Task DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
