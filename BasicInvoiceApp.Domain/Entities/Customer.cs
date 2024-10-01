using BasicInvoiceApp.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace BasicInvoiceApp.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }

        public List<Invoice> Invoices { get; set; }
    }
}
