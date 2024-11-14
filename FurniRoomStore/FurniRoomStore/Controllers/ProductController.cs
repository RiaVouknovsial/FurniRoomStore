using FurniRoomStore.Models;
using FurniRoomStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FurniRoomStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // Получение всех продуктов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                _logger.LogInformation("Получены все продукты.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех продуктов.");
                return StatusCode(500, "Ошибка сервера при получении всех продуктов.");
            }
        }

        // Получение продукта по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Продукт с ID {id} не найден.");
                    return NotFound();
                }
                _logger.LogInformation($"Получен продукт с ID {id}.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении продукта с ID {id}.");
                return StatusCode(500, "Ошибка сервера при получении продукта.");
            }
        }

        // Добавление нового продукта
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product, [FromForm] IFormFile imageFile)
        {
            try
            {
                byte[] imageData;
                using (var ms = new MemoryStream())
                {
                    await imageFile.CopyToAsync(ms);
                    imageData = ms.ToArray();
                }

                await _productService.AddProductAsync(product, imageData);
                _logger.LogInformation($"Продукт с ID {product.Id} успешно добавлен.");
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении нового продукта.");
                return StatusCode(500, "Ошибка сервера при добавлении нового продукта.");
            }
        }

        // Обновление существующего продукта
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    _logger.LogWarning($"Несоответствие ID продукта: ожидаемый ID {id}, фактический ID {product.Id}.");
                    return BadRequest("Product ID mismatch");
                }

                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    _logger.LogWarning($"Продукт с ID {id} не найден для обновления.");
                    return NotFound();
                }

                await _productService.UpdateProductAsync(product);
                _logger.LogInformation($"Продукт с ID {id} успешно обновлен.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении продукта с ID {id}.");
                return StatusCode(500, "Ошибка сервера при обновлении продукта.");
            }
        }

        // Удаление продукта
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Продукт с ID {id} не найден для удаления.");
                    return NotFound();
                }

                await _productService.DeleteProductAsync(id);
                _logger.LogInformation($"Продукт с ID {id} успешно удален.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении продукта с ID {id}.");
                return StatusCode(500, "Ошибка сервера при удалении продукта.");
            }
        }
    }
}
