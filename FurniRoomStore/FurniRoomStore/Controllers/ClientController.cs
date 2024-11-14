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
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        // Получение всех клиентов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                _logger.LogInformation("Получены все клиенты.");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех клиентов.");
                return StatusCode(500, "Ошибка сервера при получении клиентов.");
            }
        }

        // Получение клиента по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                {
                    _logger.LogWarning($"Клиент с ID {id} не найден.");
                    return NotFound();
                }
                _logger.LogInformation($"Получен клиент с ID {id}.");
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении клиента с ID {id}.");
                return StatusCode(500, "Ошибка сервера при получении клиента.");
            }
        }

        // Добавление нового клиента
        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody] Client client)
        {
            try
            {
                await _clientService.AddClientAsync(client);
                _logger.LogInformation($"Клиент с ID {client.Id} успешно добавлен.");
                return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении клиента.");
                return StatusCode(500, "Ошибка сервера при добавлении клиента.");
            }
        }

        // Обновление существующего клиента
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            try
            {
                if (id != client.Id)
                {
                    _logger.LogWarning($"Нев match ID клиента. Ожидаемый ID: {id}, фактический ID: {client.Id}.");
                    return BadRequest("Client ID mismatch");
                }

                var existingClient = await _clientService.GetClientByIdAsync(id);
                if (existingClient == null)
                {
                    _logger.LogWarning($"Клиент с ID {id} не найден для обновления.");
                    return NotFound();
                }

                await _clientService.UpdateClientAsync(client);
                _logger.LogInformation($"Клиент с ID {id} успешно обновлен.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении клиента с ID {id}.");
                return StatusCode(500, "Ошибка сервера при обновлении клиента.");
            }
        }

        // Удаление клиента
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                {
                    _logger.LogWarning($"Клиент с ID {id} не найден для удаления.");
                    return NotFound();
                }

                await _clientService.DeleteClientAsync(id);
                _logger.LogInformation($"Клиент с ID {id} успешно удален.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении клиента с ID {id}.");
                return StatusCode(500, "Ошибка сервера при удалении клиента.");
            }
        }

        // Поиск клиентов по полному имени
        [HttpGet("search/fullname")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByFullName([FromQuery] string fullName)
        {
            try
            {
                var clients = await _clientService.GetClientsByFullNameAsync(fullName);
                _logger.LogInformation($"Поиск клиентов по полному имени: {fullName}");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске клиентов по полному имени.");
                return StatusCode(500, "Ошибка сервера при поиске клиентов по полному имени.");
            }
        }

        // Поиск клиентов по дате регистрации
        [HttpGet("search/registrationdate")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByRegistrationDate([FromQuery] DateTime registrationDate)
        {
            try
            {
                var clients = await _clientService.GetClientsByRegistrationDateAsync(registrationDate);
                _logger.LogInformation($"Поиск клиентов по дате регистрации: {registrationDate.ToShortDateString()}");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске клиентов по дате регистрации.");
                return StatusCode(500, "Ошибка сервера при поиске клиентов по дате регистрации.");
            }
        }

        // Поиск клиентов по стране
        [HttpGet("search/country")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByCountry([FromQuery] string country)
        {
            try
            {
                var clients = await _clientService.GetClientsByCountryAsync(country);
                _logger.LogInformation($"Поиск клиентов по стране: {country}");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске клиентов по стране.");
                return StatusCode(500, "Ошибка сервера при поиске клиентов по стране.");
            }
        }

        // Поиск клиентов по городу
        [HttpGet("search/city")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByCity([FromQuery] string city)
        {
            try
            {
                var clients = await _clientService.GetClientsByCityAsync(city);
                _logger.LogInformation($"Поиск клиентов по городу: {city}");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске клиентов по городу.");
                return StatusCode(500, "Ошибка сервера при поиске клиентов по городу.");
            }
        }
    }
}
