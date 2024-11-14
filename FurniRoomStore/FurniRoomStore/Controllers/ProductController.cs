using FurniRoomStore.Models;
using FurniRoomStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurniRoomStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Получение всех продуктов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // Получение продукта по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // Добавление нового продукта
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product, [FromForm] IFormFile imageFile)
        {
            byte[] imageData;
            using (var ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                imageData = ms.ToArray();
            }
            await _productService.AddProductAsync(product, imageData);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        // Обновление существующего продукта
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch");

            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // Удаление продукта
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
