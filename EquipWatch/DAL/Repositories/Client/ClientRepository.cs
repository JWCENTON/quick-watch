using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Client;

public class ClientRepository : IClientRepository
{
    private readonly DatabaseContext _context;

    public ClientRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
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

    public async Task CreateAsync(Domain.Client.Models.Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.Client.Models.Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}