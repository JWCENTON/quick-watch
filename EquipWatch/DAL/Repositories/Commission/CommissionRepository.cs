using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Commission;

public class CommissionRepository : ICommissionRepository
{
    private readonly DatabaseContext _context;

    public CommissionRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
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
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.Commission.Models.Commission.Commission entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}