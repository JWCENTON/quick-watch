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

    public async Task<List<Domain.Client.Models.Client>> GetAll()
    {
        return await _context.Client.ToListAsync();
    }

    public async Task<Domain.Client.Models.Client> Get(Guid id)
    {
        return await _context.Client.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Create(Domain.Client.Models.Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Domain.Client.Models.Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}