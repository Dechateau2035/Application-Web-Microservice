
using Microsoft.EntityFrameworkCore;
using System;

namespace MicroServiceClientProduit.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext context;

        public ClientRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Client> AddClient(Client client)
        {
            var result = await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> DeleteClient(int id)
        {
            var result = await context.Clients.FirstOrDefaultAsync(cl => cl.ClientId == id);

            if (result != null)
            {
                context.Clients.Remove(result);
                await context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Client> GetClient(int id)
        {
            return await context.Clients.FirstOrDefaultAsync(cl => cl.ClientId == id);
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            return await context.Clients.FirstOrDefaultAsync(cl => cl.EmailAddress == email);
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await context.Clients.ToListAsync();
        }

        public async Task<IEnumerable<Client>> Search(string name)
        {
            IQueryable<Client> query = context.Clients;
            if (!string.IsNullOrEmpty(name))
                query = query.Where(cl => cl.FirstName.Contains(name) || cl.LastName.Contains(name));
            return await query.ToListAsync();
        }

        public async Task<Client> UpdateClient(Client client)
        {
            var result = await context.Clients.FirstOrDefaultAsync(cl => cl.ClientId == client.ClientId);

            if (result != null)
            {
                result.FirstName = client.FirstName;
                result.LastName = client.LastName;
                result.EmailAddress = client.EmailAddress;

                await context.SaveChangesAsync();
            }
            return result;
        }
    }
}
