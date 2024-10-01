namespace BasicInvoiceApp.Application.DTOs.invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }
}
