namespace BasicInvoiceApp.Application.DTOs.customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
