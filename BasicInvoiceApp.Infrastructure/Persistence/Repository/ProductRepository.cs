using BasicInvoiceApp.Domain.Entities;
using BasicInvoiceApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BasicInvoiceApp.Infrastructure.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InvoiceDbContext _context;
        public ProductRepository(InvoiceDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates the price of a product by its ID, leaving other properties unchanged.
        /// </summary>
        public async Task UpdateProductPriceAsync(int id, decimal newPrice)
        {
            var product = new Product
            {
                Id = id,
                Price = newPrice
            };
            _context.Products.Attach(product);

            _context.Entry(product).Property(p => p.Price).IsModified = true;

            await _context.SaveChangesAsync();
        }
    }
}
