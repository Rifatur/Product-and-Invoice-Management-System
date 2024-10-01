using BasicInvoiceApp.Application.DTOs.customer;
using BasicInvoiceApp.Application.Interfaces;
using BasicInvoiceApp.Domain.Entities;
using BasicInvoiceApp.Domain.Repositories;

namespace BasicInvoiceApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> AddCustomerAsync(CreateCustomerDto customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                Address = customerDto.Address
            };
            await _customerRepository.AddAsync(customer);

            return new CustomerDto { Id = customer.Id, Name = customer.Name, Address = customer.Address };
        }

        public Task DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address
            });
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address
            };
        }

        public Task UpdateCustomerAsync(UpdateCustomerDto customerDto)
        {
            throw new NotImplementedException();
        }
    }
}
