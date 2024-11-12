using FurniRoomStore.Interfaces;
using FurniRoomStore.Models;

namespace FurniRoomStore.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _orderRepository.GetOrdersByDateAsync(date);
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _orderRepository.GetOrdersByClientIdAsync(clientId);
        }

        public async Task<decimal> GetOrderTotalInUAHAsync(int orderId)
        {
            return await _orderRepository.GetOrderTotalInUAHAsync(orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}
