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
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // Получение всех клиентов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        // Получение клиента по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        // Добавление нового клиента
        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody] Client client)
        {
            await _clientService.AddClientAsync(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        // Обновление существующего клиента
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest("Client ID mismatch");

            var existingClient = await _clientService.GetClientByIdAsync(id);
            if (existingClient == null)
                return NotFound();

            await _clientService.UpdateClientAsync(client);
            return NoContent();
        }

        // Удаление клиента
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();

            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }

        // Поиск клиентов по полному имени
        [HttpGet("search/fullname")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByFullName([FromQuery] string fullName)
        {
            var clients = await _clientService.GetClientsByFullNameAsync(fullName);
            return Ok(clients);
        }

        // Поиск клиентов по дате регистрации
        [HttpGet("search/registrationdate")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByRegistrationDate([FromQuery] DateTime registrationDate)
        {
            var clients = await _clientService.GetClientsByRegistrationDateAsync(registrationDate);
            return Ok(clients);
        }

        // Поиск клиентов по стране
        [HttpGet("search/country")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByCountry([FromQuery] string country)
        {
            var clients = await _clientService.GetClientsByCountryAsync(country);
            return Ok(clients);
        }

        // Поиск клиентов по городу
        [HttpGet("search/city")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByCity([FromQuery] string city)
        {
            var clients = await _clientService.GetClientsByCityAsync(city);
            return Ok(clients);
        }
    }
}
