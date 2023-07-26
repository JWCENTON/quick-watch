using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Client
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _context;

        public ClientRepository(DatabaseContext context, IdentityContext identityContext)
        {
            _context = context;
            Seed.IfDbEmptyAddNewItems(context, identityContext);
        }

        public async Task<List<Domain.Client.Models.Client>> GetAllAsync()
        {
            return await _context.Client.Include(client => client.Company).ToListAsync();
        }

        public async Task<Domain.Client.Models.Client> GetAsync(Guid id)
        {
            var client = await _context.Client.Include(client => client.Company).FirstOrDefaultAsync(c => c.Id == id);
            return client ?? throw new KeyNotFoundException("Client With given Id was not found");
        }

        public async Task CreateAsync(Domain.Client.Models.Client client)
        {
            await _context.Client.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Client.Models.Client client)
        {
            _context.Client.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}