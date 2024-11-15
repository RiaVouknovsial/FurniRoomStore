using FurniRoomStore.Interfaces;
using FurniRoomStore.Models;

namespace FurniRoomStore.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                _logger.LogInformation("Получены все заказы.");
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех заказов.");
                throw new Exception("Произошла ошибка при получении всех заказов.");
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning($"Заказ с ID {id} не найден.");
                }
                else
                {
                    _logger.LogInformation($"Получен заказ с ID {id}.");
                }
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении заказа с ID {id}.");
                throw new Exception($"Произошла ошибка при получении заказа с ID {id}.");
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByDateAsync(date);
                _logger.LogInformation($"Найдено {orders} заказов на {date:yyyy-MM-dd}.");
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении заказов по дате {date:yyyy-MM-dd}.");
                throw new Exception($"Произошла ошибка при получении заказов по дате {date:yyyy-MM-dd}.");
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByClientIdAsync(clientId);
                _logger.LogInformation($"Найдено {orders} заказов для клиента с ID {clientId}.");
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении заказов для клиента с ID {clientId}.");
                throw new Exception($"Произошла ошибка при получении заказов для клиента с ID {clientId}.");
            }
        }

        public async Task<decimal> GetOrderTotalInUAHAsync(int orderId)
        {
            try
            {
                var total = await _orderRepository.GetOrderTotalInUAHAsync(orderId);
                _logger.LogInformation($"Получена общая сумма заказа с ID {orderId}: {total} UAH.");
                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении общей суммы заказа с ID {orderId}.");
                throw new Exception($"Произошла ошибка при получении общей суммы заказа с ID {orderId}.");
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.AddAsync(order);
                _logger.LogInformation($"Заказ с ID {order.Id} успешно добавлен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении заказа.");
                throw new Exception("Произошла ошибка при добавлении заказа.");
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateAsync(order);
                _logger.LogInformation($"Заказ с ID {order.Id} успешно обновлен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении заказа с ID {order.Id}.");
                throw new Exception($"Произошла ошибка при обновлении заказа с ID {order.Id}.");
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                await _orderRepository.DeleteAsync(id);
                _logger.LogInformation($"Заказ с ID {id} успешно удален.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении заказа с ID {id}.");
                throw new Exception($"Произошла ошибка при удалении заказа с ID {id}.");
            }
        }
    }
}
