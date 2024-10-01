using BasicInvoiceApp.Application.Interfaces;
using BasicInvoiceApp.Application.Services;
using BasicInvoiceApp.Domain.Repositories;
using BasicInvoiceApp.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BasicInvoiceApp.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application Product services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Register application Customer services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Register application Invoices services
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            // Register other services as needed
            services.AddScoped<InvoiceReportService>();


            return services;
        }
    }
}
