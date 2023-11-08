using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Commission;

public class CommissionRepository : ICommissionRepository
{
    private readonly DatabaseContext _context;

    public CommissionRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.Commission.Models.Commission.Commission>> GetAllAsync()
    {
        return await _context.Commissions.ToListAsync();
    }

    public async Task<Domain.Commission.Models.Commission.Commission> GetAsync(Guid id)
    {
        var commissions = await _context.Commissions.FirstOrDefaultAsync(c => c.Id == id);
        return commissions ?? throw new KeyNotFoundException("Commission with given Id was not found");
    }

    public async Task CreateAsync(Domain.Commission.Models.Commission.Commission entity)
    {
        await _context.Commissions.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Commission.Models.Commission.Commission entity)
    {
        _context.Commissions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var commission = await _context.Commissions.FindAsync(id);
        if (commission != null)
        {
            _context.Commissions.Remove(commission);
            await _context.SaveChangesAsync();
        }
    }
}