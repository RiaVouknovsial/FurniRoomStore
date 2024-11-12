using FurniRoomStore.Interfaces;
using FurniRoomStore.Models;

namespace FurniRoomStore.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task AddClientAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        // Поиск клиентов по полному имени
        public async Task<IEnumerable<Client>> GetClientsByFullNameAsync(string fullName)
        {
            return await _clientRepository.GetClientsByFullNameAsync(fullName);
        }

        // Поиск клиентов по дате регистрации
        public async Task<IEnumerable<Client>> GetClientsByRegistrationDateAsync(DateTime registrationDate)
        {
            return await _clientRepository.GetClientsByRegistrationDateAsync(registrationDate);
        }

        // Поиск клиентов по стране
        public async Task<IEnumerable<Client>> GetClientsByCountryAsync(string country)
        {
            return await _clientRepository.GetClientsByCountryAsync(country);
        }

        // Поиск клиентов по городу
        public async Task<IEnumerable<Client>> GetClientsByCityAsync(string city)
        {
            return await _clientRepository.GetClientsByCityAsync(city);
        }
    }
}
