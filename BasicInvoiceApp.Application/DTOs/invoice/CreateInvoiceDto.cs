using System.ComponentModel.DataAnnotations;

namespace BasicInvoiceApp.Application.DTOs.invoice
{
    public class CreateInvoiceDto
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public List<CreateInvoiceItemDto> Items { get; set; }
    }
}
