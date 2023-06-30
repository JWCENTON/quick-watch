using DAL;
using Domain.Client.Models;
using Microsoft.EntityFrameworkCore;
using webapi.Entities.BaseServices;

namespace webapi.Entities.ClientApi.Services;

public class ClientService : IClientService
{
    private readonly DatabaseContext _context;

    public ClientService(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Client>> GetAll()
    {
        return await _context.Client.ToListAsync();
    }

    public async Task<Client> Get(Guid id)
    {
        return await _context.Client.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Create(Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}