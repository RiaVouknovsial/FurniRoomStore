using FurniRoomStore.Models;
using FurniRoomStore.Repositories;

namespace FurniRoomStore.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(FurniRoomStoreContext context) : base(context) { }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dbSet.FindAsync(productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _dbSet.FindAsync(product.Id);
            if (existingProduct != null)
            {
                // Обновляем свойства продукта
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.Category = product.Category;
                await _context.SaveChangesAsync();
            }
        }
    }


}
