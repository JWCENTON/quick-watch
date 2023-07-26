using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.WorksOn;

public class WorksOnRepository : IWorksOnRepository
{
    private readonly DatabaseContext _context;

    public WorksOnRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.WorksOn.Models.WorksOn>> GetAllAsync()
    {
        return await _context.WorksOn.ToListAsync();
    }
}