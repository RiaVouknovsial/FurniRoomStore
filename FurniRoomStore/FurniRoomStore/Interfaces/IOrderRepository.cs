using FurniRoomStore.Models;
using FurniRoomStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FurniRoomStore.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date); // Метод поиска заказов по дате
        Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId); // Метод поиска заказов по клиенту
        Task<decimal> GetOrderTotalInUAHAsync(int orderId); // Метод для расчета суммы заказа в гривнах
    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(FurniRoomStoreContext context) : base(context) { }

        // Метод для поиска заказов по дате
        public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _dbSet
                .Where(order => order.OrderDate.Date == date.Date)
                .ToListAsync();
        }

        // Метод для поиска заказов по ID клиента
        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _dbSet
                .Where(order => order.ClientId == clientId)
                .ToListAsync();
        }

        // Метод для расчета общей суммы заказа в гривнах
        public async Task<decimal> GetOrderTotalInUAHAsync(int orderId)
        {
            var order = await _dbSet.FindAsync(orderId);
            return order != null ? order.Quantity * order.OrderPriceInUAH : 0;
        }
    }
}
