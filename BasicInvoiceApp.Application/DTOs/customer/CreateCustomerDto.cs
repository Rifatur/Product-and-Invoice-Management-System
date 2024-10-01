using System.ComponentModel.DataAnnotations;

namespace BasicInvoiceApp.Application.DTOs.customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
