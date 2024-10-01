using System.ComponentModel.DataAnnotations;

namespace BasicInvoiceApp.Application.DTOs.customer
{
    public class UpdateCustomerDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
