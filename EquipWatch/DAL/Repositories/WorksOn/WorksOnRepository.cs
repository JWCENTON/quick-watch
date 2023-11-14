using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.WorksOn;

public class WorksOnRepository : IWorksOnRepository
{
    private readonly DatabaseContext _context;
    private readonly IdentityContext _identityContext;

    public WorksOnRepository(DatabaseContext context, IdentityContext identityContext)
    {
        _context = context;
        _identityContext = identityContext;
        Seed.IfDbEmptyAddNewItems(context, identityContext);
    }

    public async Task<List<Domain.WorksOn.Models.WorksOn>> GetAllAsync()
    {
        return await _context.WorksOn.ToListAsync();
    }

    public Task<Domain.WorksOn.Models.WorksOn> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Domain.WorksOn.Models.WorksOn entity)
    {
        if (_context.WorksOn.Any(w => w.UserId == entity.UserId && w.CommissionId == entity.CommissionId && w.EndTime == null))
        {
            throw new ArgumentException("Worker is already assigned to this commission");
        }
        _context.WorksOn.AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Domain.WorksOn.Models.WorksOn entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.User.Models.User>> GetCommissionAssignedEmployeesAsync(Guid commissionId)
    {
        var worksOnList = _context.WorksOn.Where(w => w.CommissionId == commissionId && w.EndTime == null);
        var userIds = worksOnList.Select(w => w.UserId);
        var users = new List<Domain.User.Models.User>();
        foreach (var userId in userIds)
        {
            users.Add(_identityContext.Users.First(user => user.Id == userId));
        }
        return Task.FromResult(users);
    }
    public async Task<List<Domain.WorksOn.Models.WorksOn>> GeCurrentWorksOnByCommissionIdAsync(Guid commissionId)
    {
        var worksOnList = await _context.WorksOn.Where(w => w.CommissionId == commissionId && w.EndTime == null).ToListAsync();
        return worksOnList;
    }
}