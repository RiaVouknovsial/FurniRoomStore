using FurniRoomStore.Models;
using FurniRoomStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FurniRoomStore.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // Получить все продукты
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Запрос на получение всех продуктов.");
                var products = await _productService.GetAllProductsAsync();
                _logger.LogInformation($"Возвращено {products.Count()} продуктов.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех продуктов.");
                return StatusCode(500, new { message = "Произошла ошибка при получении всех продуктов." });
            }
        }

        // Получить продукт по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на получение продукта с ID {id}.");
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Продукт с ID {id} не найден.");
                    return NotFound(new { message = "Продукт не найден" });
                }

                _logger.LogInformation($"Продукт с ID {id} успешно найден.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении продукта с ID {id}.");
                return StatusCode(500, new { message = $"Произошла ошибка при получении продукта с ID {id}." });
            }
        }

        // Добавить новый продукт
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                _logger.LogInformation("Запрос на добавление нового продукта.");
                await _productService.AddProductAsync(product);
                _logger.LogInformation($"Продукт с ID {product.Id} успешно добавлен.");
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении продукта.");
                return StatusCode(500, new { message = "Произошла ошибка при добавлении продукта." });
            }
        }

        // Обновить продукт
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                _logger.LogWarning($"ID продукта {id} не совпадает с ID в теле запроса.");
                return BadRequest(new { message = "ID продукта не совпадает с ID в запросе" });
            }

            try
            {
                _logger.LogInformation($"Запрос на обновление продукта с ID {id}.");
                await _productService.UpdateProductAsync(product);
                _logger.LogInformation($"Продукт с ID {id} успешно обновлен.");
                return NoContent(); // Статус 204 (No Content) при успешном обновлении
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении продукта с ID {id}.");
                return StatusCode(500, new { message = $"Произошла ошибка при обновлении продукта с ID {id}." });
            }
        }

        // Удалить продукт
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation($"Запрос на удаление продукта с ID {id}.");
                await _productService.DeleteProductAsync(id);
                _logger.LogInformation($"Продукт с ID {id} успешно удален.");
                return NoContent(); // Статус 204 (No Content) при успешном удалении
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении продукта с ID {id}.");
                return StatusCode(500, new { message = $"Произошла ошибка при удалении продукта с ID {id}." });
            }
        }
    }
}
