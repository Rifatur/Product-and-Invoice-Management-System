using BasicInvoiceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicInvoiceApp.Infrastructure.Persistence.Repository
{
    public class InvoiceReportService
    {
        private readonly InvoiceDbContext _context;
        public InvoiceReportService(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetInvoiceDataAsync(int invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
        }
    }
}
