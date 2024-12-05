using ClientAPI.Model;

namespace ClientAPI.Repositorie
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClient(int id);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client client);
        Task<Client> DeleteClient(int id);
        Task<Client> GetClientByEmail(string email);
        Task<IEnumerable<Client>> Search(string name);
    }
}
