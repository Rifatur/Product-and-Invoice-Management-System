using BasicInvoiceApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicInvoiceApp.Domain.ValueObjects
{
    public class InvoiceItem
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public decimal Total { get; set; }
    }
}
