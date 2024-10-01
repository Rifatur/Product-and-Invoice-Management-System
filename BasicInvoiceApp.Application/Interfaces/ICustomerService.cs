using BasicInvoiceApp.Application.DTOs.customer;

namespace BasicInvoiceApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> AddCustomerAsync(CreateCustomerDto customerDto);
        Task UpdateCustomerAsync(UpdateCustomerDto customerDto);
        Task DeleteCustomerAsync(int id);
    }
}
