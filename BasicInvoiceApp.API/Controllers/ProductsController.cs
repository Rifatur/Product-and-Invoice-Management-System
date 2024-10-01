using BasicInvoiceApp.Application.DTOs;
using BasicInvoiceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicInvoiceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest("Product ID mismatch");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _productService.UpdateProductAsync(productDto);
            return NoContent();
        }

        // PATCH: api/Products/5/UpdatePrice
        [HttpPatch("{id}/UpdatePrice")]
        public async Task<IActionResult> UpdateProductPrice(int id, [FromBody] decimal newPrice)
        {
            if (newPrice < 0) return BadRequest("Price cannot be negative");

            await _productService.UpdateProductPriceAsync(id, newPrice);
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
