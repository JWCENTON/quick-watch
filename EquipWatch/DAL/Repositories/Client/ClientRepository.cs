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
        return await _context.Client.ToListAsync();
    }

    public async Task<Domain.Client.Models.Client> GetAsync(Guid id)
    {
        return await _context.Client.FirstOrDefaultAsync(c => c.Id == id);
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