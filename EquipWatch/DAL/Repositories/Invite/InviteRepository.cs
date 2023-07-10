using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Invite;

public class InviteRepository : IInviteRepository
{
    private readonly DatabaseContext _context;

    public InviteRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Domain.Invite.Models.Invite>> GetAllAsync()
    {
        return await _context.Invites.ToListAsync();
    }

    public async Task<Domain.Invite.Models.Invite> GetAsync(Guid id)
    {
        var client = await _context.Invites.FirstOrDefaultAsync(c => c.Id == id);
        return client ?? throw new KeyNotFoundException("Invite With given Id was not found");
    }

    public async Task CreateAsync(Domain.Invite.Models.Invite entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.Invite.Models.Invite entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}