using BasicInvoiceApp.Application.DTOs.invoice;
using BasicInvoiceApp.Application.Helper;
using BasicInvoiceApp.Application.Interfaces;
using BasicInvoiceApp.Domain.Entities;
using BasicInvoiceApp.Domain.Repositories;
using BasicInvoiceApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BasicInvoiceApp.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            return new InvoiceDto
            {
                Id = invoice.Id,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                TotalAmount = invoice.TotalAmount,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Total = i.Total
                }).ToList()
            };
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices.Select(invoice => new InvoiceDto
            {
                Id = invoice.Id,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                TotalAmount = invoice.TotalAmount,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Total = i.Total
                }).ToList()
            });
        }

        public async Task<InvoiceDto> AddInvoiceAsync(CreateInvoiceDto invoiceDto)
        {
            var invoice = new Invoice
            {
                Date = invoiceDto.Date,
                CustomerId = invoiceDto.CustomerId,
                TotalAmount = invoiceDto.Items.Sum(i => i.Quantity * i.Price),
                Items = invoiceDto.Items.Select(i => new InvoiceItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Total = i.Quantity * i.Price
                }).ToList()
            };

            await _invoiceRepository.AddAsync(invoice);

            return new InvoiceDto
            {
                Id = invoice.Id,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                TotalAmount = invoice.TotalAmount,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Total = i.Total
                }).ToList()
            };
        }

        public async Task UpdateInvoiceAsync(InvoiceDto invoiceDto)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceDto.Id);
            invoice.Date = invoiceDto.Date;
            invoice.CustomerId = invoiceDto.CustomerId;
            invoice.TotalAmount = invoiceDto.TotalAmount;
            invoice.Items = invoiceDto.Items.Select(i => new InvoiceItem
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                Price = i.Price,
                Total = i.Total
            }).ToList();

            await _invoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            await _invoiceRepository.DeleteAsync(id);
        }

        public async Task<PaginatedList<InvoiceDto>> GetInvoicesByCustomerIdAsync(int customerId, int pageNumber, int pageSize, DateTime? startDate, DateTime? endDate)
        {
            var query = _invoiceRepository.Query()
                .Where(i => i.CustomerId == customerId);

            if (startDate.HasValue)
            {
                query = query.Where(i => i.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(i => i.Date <= endDate.Value);
            }

            var totalRecords = await query.CountAsync();
            var invoices = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var invoiceDtos = invoices.Select(invoice => new InvoiceDto
            {
                Id = invoice.Id,
                Date = invoice.Date,
                CustomerId = invoice.CustomerId,
                TotalAmount = invoice.TotalAmount,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Total = i.Total
                }).ToList()
            }).ToList();

            return new PaginatedList<InvoiceDto>(invoiceDtos, totalRecords, pageNumber, pageSize);
        }
    }
}
