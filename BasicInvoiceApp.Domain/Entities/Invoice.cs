using BasicInvoiceApp.Domain.Entities.Common;
using BasicInvoiceApp.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicInvoiceApp.Domain.Entities
{
    public class Invoice : BaseAuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public List<InvoiceItem> Items { get; set; }
    }
}
