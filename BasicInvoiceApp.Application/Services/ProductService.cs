using BasicInvoiceApp.Application.DTOs;
using BasicInvoiceApp.Application.Interfaces;
using BasicInvoiceApp.Domain.Entities;
using BasicInvoiceApp.Domain.Repositories;

namespace BasicInvoiceApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            };
            await _productRepository.AddAsync(product);
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            };
            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateProductPriceAsync(int id, decimal newPrice)
        {
            await _productRepository.UpdateProductPriceAsync(id, newPrice);
        }
    }
}
