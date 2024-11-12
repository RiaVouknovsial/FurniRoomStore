using FurniRoomStore.Models;
using FurniRoomStore.Repositories;

namespace FurniRoomStore.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        // Можно добавить дополнительные методы, если они необходимы
    }

    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(FurniRoomStoreContext context) : base(context) { }
    }
}
