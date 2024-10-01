using BasicInvoiceApp.Application.DTOs.customer;
using BasicInvoiceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicInvoiceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(CreateCustomerDto customerDto)
        {
            var createdCustomer = await _customerService.AddCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);
        }

    }
}
