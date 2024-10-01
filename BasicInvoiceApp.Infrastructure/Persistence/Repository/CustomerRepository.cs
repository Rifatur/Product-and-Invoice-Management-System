using BasicInvoiceApp.Domain.Entities;
using BasicInvoiceApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BasicInvoiceApp.Infrastructure.Persistence.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InvoiceDbContext _context;

        public CustomerRepository(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Include(c => c.Invoices).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.Invoices).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
