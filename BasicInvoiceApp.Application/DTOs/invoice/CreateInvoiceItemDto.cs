using System.ComponentModel.DataAnnotations;

namespace BasicInvoiceApp.Application.DTOs.invoice
{
    public class CreateInvoiceItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
