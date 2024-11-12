using FurniRoomStore.Models;
using FurniRoomStore.Repositories;

namespace FurniRoomStore.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<byte[]> GetImageAsync(int productId);
        Task UpdateImageAsync(int productId, byte[] imageData);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(FurniRoomStoreContext context) : base(context) { }

        public async Task<byte[]> GetImageAsync(int productId)
        {
            var product = await _dbSet.FindAsync(productId);
            return product?.ImageData;
        }

        public async Task UpdateImageAsync(int productId, byte[] imageData)
        {
            var product = await _dbSet.FindAsync(productId);
            if (product != null)
            {
                product.ImageData = imageData;
                await _context.SaveChangesAsync();
            }
        }
    }
}
