using FurniRoomStore.Models;
using FurniRoomStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FurniRoomStore.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientsByFullNameAsync(string fullName); // Поиск по полному имени
        Task<IEnumerable<Client>> GetClientsByRegistrationDateAsync(DateTime registrationDate); // Поиск по дате регистрации
        Task<IEnumerable<Client>> GetClientsByCountryAsync(string country); // Поиск по стране
        Task<IEnumerable<Client>> GetClientsByCityAsync(string city); // Поиск по городу
    }

    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(FurniRoomStoreContext context) : base(context) { }

        // Поиск клиентов по полному имени
        public async Task<IEnumerable<Client>> GetClientsByFullNameAsync(string fullName)
        {
            return await _dbSet
                .Where(client => client.FullName == fullName)
                .ToListAsync();
        }

        // Поиск клиентов по дате регистрации
        public async Task<IEnumerable<Client>> GetClientsByRegistrationDateAsync(DateTime registrationDate)
        {
            return await _dbSet
                .Where(client => client.RegistrationDate.Date == registrationDate.Date)
                .ToListAsync();
        }

        // Поиск клиентов по стране
        public async Task<IEnumerable<Client>> GetClientsByCountryAsync(string country)
        {
            return await _dbSet
                .Where(client => client.Country == country)
                .ToListAsync();
        }

        // Поиск клиентов по городу
        public async Task<IEnumerable<Client>> GetClientsByCityAsync(string city)
        {
            return await _dbSet
                .Where(client => client.City == city)
                .ToListAsync();
        }
    }
}
