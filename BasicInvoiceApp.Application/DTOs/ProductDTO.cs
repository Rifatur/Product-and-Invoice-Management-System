namespace BasicInvoiceApp.Application.DTOs
{


    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
    //public record ProductDTO(
    //        int Id,
    //        string Name,
    //        decimal Price,
    //        int Stock
    //    );

}
