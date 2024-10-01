using BasicInvoiceApp.Domain.Entities.Common;
using BasicInvoiceApp.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace BasicInvoiceApp.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10,000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value.")]
        public int Stock { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
