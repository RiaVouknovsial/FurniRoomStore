using FurniRoomStore.Interfaces;
using FurniRoomStore.Models;

namespace FurniRoomStore.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();
                _logger.LogInformation("Получены все клиенты.");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех клиентов.");
                throw new Exception("Произошла ошибка при получении клиентов.");
            }
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client == null)
                {
                    _logger.LogWarning($"Клиент с ID {id} не найден.");
                }
                else
                {
                    _logger.LogInformation($"Получен клиент с ID {id}.");
                }
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении клиента с ID {id}.");
                throw new Exception($"Произошла ошибка при получении клиента с ID {id}.");
            }
        }

        public async Task AddClientAsync(Client client)
        {
            try
            {
                await _clientRepository.AddAsync(client);
                _logger.LogInformation($"Клиент с ID {client.Id} успешно добавлен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении клиента.");
                throw new Exception("Произошла ошибка при добавлении клиента.");
            }
        }

        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                await _clientRepository.UpdateAsync(client);
                _logger.LogInformation($"Клиент с ID {client.Id} успешно обновлен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении клиента с ID {client.Id}.");
                throw new Exception($"Произошла ошибка при обновлении клиента с ID {client.Id}.");
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            try
            {
                await _clientRepository.DeleteAsync(id);
                _logger.LogInformation($"Клиент с ID {id} успешно удален.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении клиента с ID {id}.");
                throw new Exception($"Произошла ошибка при удалении клиента с ID {id}.");
            }
        }

        // Поиск клиентов по полному имени
        public async Task<IEnumerable<Client>> GetClientsByFullNameAsync(string fullName)
        {
            try
            {
                var clients = await _clientRepository.GetClientsByFullNameAsync(fullName);
                _logger.LogInformation($"Найдено {clients} клиентов с именем '{fullName}'.");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске клиентов по имени '{fullName}'.");
                throw new Exception($"Произошла ошибка при поиске клиентов по имени '{fullName}'.");
            }
        }

        // Поиск клиентов по дате регистрации
        public async Task<IEnumerable<Client>> GetClientsByRegistrationDateAsync(DateTime registrationDate)
        {
            try
            {
                var clients = await _clientRepository.GetClientsByRegistrationDateAsync(registrationDate);
                _logger.LogInformation($"Найдено {clients} клиентов, зарегистрированных на {registrationDate:yyyy-MM-dd}.");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске клиентов по дате регистрации '{registrationDate:yyyy-MM-dd}'.");
                throw new Exception($"Произошла ошибка при поиске клиентов по дате регистрации '{registrationDate:yyyy-MM-dd}'.");
            }
        }

        // Поиск клиентов по стране
        public async Task<IEnumerable<Client>> GetClientsByCountryAsync(string country)
        {
            try
            {
                var clients = await _clientRepository.GetClientsByCountryAsync(country);
                _logger.LogInformation($"Найдено {clients} клиентов из страны '{country}'.");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске клиентов по стране '{country}'.");
                throw new Exception($"Произошла ошибка при поиске клиентов по стране '{country}'.");
            }
        }

        // Поиск клиентов по городу
        public async Task<IEnumerable<Client>> GetClientsByCityAsync(string city)
        {
            try
            {
                var clients = await _clientRepository.GetClientsByCityAsync(city);
                _logger.LogInformation($"Найдено {clients} клиентов из города '{city}'.");
                return clients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при поиске клиентов по городу '{city}'.");
                throw new Exception($"Произошла ошибка при поиске клиентов по городу '{city}'.");
            }
        }
    }
}
