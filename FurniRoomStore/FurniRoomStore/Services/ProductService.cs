using FurniRoomStore.Interfaces;
using FurniRoomStore.Models;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();
            _logger.LogInformation("Получены все продукты.");
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении всех продуктов.");
            throw new Exception("Произошла ошибка при получении всех продуктов.");
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Продукт с ID {id} не найден.");
            }
            else
            {
                _logger.LogInformation($"Получен продукт с ID {id}.");
            }
            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении продукта с ID {id}.");
            throw new Exception($"Произошла ошибка при получении продукта с ID {id}.");
        }
    }

    public async Task AddProductAsync(Product product)
    {
        try
        {
            await _productRepository.AddAsync(product);
            _logger.LogInformation($"Продукт с ID {product.Id} успешно добавлен.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении продукта.");
            throw new Exception("Произошла ошибка при добавлении продукта.");
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            await _productRepository.UpdateAsync(product);
            _logger.LogInformation($"Продукт с ID {product.Id} успешно обновлен.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при обновлении продукта с ID {product.Id}.");
            throw new Exception($"Произошла ошибка при обновлении продукта с ID {product.Id}.");
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        try
        {
            await _productRepository.DeleteAsync(id);
            _logger.LogInformation($"Продукт с ID {id} успешно удален.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при удалении продукта с ID {id}.");
            throw new Exception($"Произошла ошибка при удалении продукта с ID {id}.");
        }
    }
}
