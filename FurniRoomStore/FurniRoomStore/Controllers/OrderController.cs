using FurniRoomStore.Models;
using FurniRoomStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FurniRoomStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(OrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // Получение всех заказов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                _logger.LogInformation("Получены все заказы.");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех заказов.");
                return StatusCode(500, "Ошибка сервера при получении заказов.");
            }
        }

        // Получение заказа по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning($"Заказ с ID {id} не найден.");
                    return NotFound();
                }
                _logger.LogInformation($"Получен заказ с ID {id}.");
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении заказа с ID {id}.");
                return StatusCode(500, "Ошибка сервера при получении заказа.");
            }
        }

        // Получение заказов по дате
        [HttpGet("search/date")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByDate([FromQuery] DateTime date)
        {
            try
            {
                var orders = await _orderService.GetOrdersByDateAsync(date);
                _logger.LogInformation($"Поиск заказов по дате {date.ToShortDateString()}.");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске заказов по дате {date.ToShortDateString()}.");
                return StatusCode(500, "Ошибка сервера при поиске заказов по дате.");
            }
        }

        // Получение заказов по ID клиента
        [HttpGet("search/client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByClientId(int clientId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByClientIdAsync(clientId);
                _logger.LogInformation($"Поиск заказов для клиента с ID {clientId}.");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске заказов для клиента с ID {clientId}.");
                return StatusCode(500, "Ошибка сервера при поиске заказов по ID клиента.");
            }
        }

        // Получение общей суммы заказа в UAH по ID заказа
        [HttpGet("{orderId}/total")]
        public async Task<ActionResult<decimal>> GetOrderTotalInUAH(int orderId)
        {
            try
            {
                var total = await _orderService.GetOrderTotalInUAHAsync(orderId);
                _logger.LogInformation($"Получена общая сумма заказа с ID {orderId}.");
                return Ok(total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении общей суммы заказа с ID {orderId}.");
                return StatusCode(500, "Ошибка сервера при получении общей суммы заказа.");
            }
        }

        // Добавление нового заказа
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] Order order)
        {
            try
            {
                await _orderService.AddOrderAsync(order);
                _logger.LogInformation($"Новый заказ с ID {order.Id} успешно добавлен.");
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении нового заказа.");
                return StatusCode(500, "Ошибка сервера при добавлении нового заказа.");
            }
        }

        // Обновление существующего заказа
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                if (id != order.Id)
                {
                    _logger.LogWarning($"Нев match ID заказа. Ожидаемый ID: {id}, фактический ID: {order.Id}.");
                    return BadRequest("Order ID mismatch");
                }

                var existingOrder = await _orderService.GetOrderByIdAsync(id);
                if (existingOrder == null)
                {
                    _logger.LogWarning($"Заказ с ID {id} не найден для обновления.");
                    return NotFound();
                }

                await _orderService.UpdateOrderAsync(order);
                _logger.LogInformation($"Заказ с ID {id} успешно обновлен.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении заказа с ID {id}.");
                return StatusCode(500, "Ошибка сервера при обновлении заказа.");
            }
        }

        // Удаление заказа
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning($"Заказ с ID {id} не найден для удаления.");
                    return NotFound();
                }

                await _orderService.DeleteOrderAsync(id);
                _logger.LogInformation($"Заказ с ID {id} успешно удален.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении заказа с ID {id}.");
                return StatusCode(500, "Ошибка сервера при удалении заказа.");
            }
        }
    }
}
