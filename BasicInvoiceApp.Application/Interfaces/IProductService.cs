using BasicInvoiceApp.Application.DTOs;

namespace BasicInvoiceApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(ProductDTO productDto);
        Task DeleteProductAsync(int id);
        Task UpdateProductPriceAsync(int id, decimal newPrice);
    }
}
