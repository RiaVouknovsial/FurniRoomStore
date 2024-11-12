using FurniRoomStore.Models;
using FurniRoomStore.Services;
using Microsoft.AspNetCore.Mvc;
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

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Получение всех заказов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Получение заказа по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        // Получение заказов по дате
        [HttpGet("search/date")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByDate([FromQuery] DateTime date)
        {
            var orders = await _orderService.GetOrdersByDateAsync(date);
            return Ok(orders);
        }

        // Получение заказов по ID клиента
        [HttpGet("search/client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByClientId(int clientId)
        {
            var orders = await _orderService.GetOrdersByClientIdAsync(clientId);
            return Ok(orders);
        }

        // Получение общей суммы заказа в UAH по ID заказа
        [HttpGet("{orderId}/total")]
        public async Task<ActionResult<decimal>> GetOrderTotalInUAH(int orderId)
        {
            var total = await _orderService.GetOrderTotalInUAHAsync(orderId);
            return Ok(total);
        }

        // Добавление нового заказа
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] Order order)
        {
            await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        // Обновление существующего заказа
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
                return BadRequest("Order ID mismatch");

            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
                return NotFound();

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        // Удаление заказа
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
