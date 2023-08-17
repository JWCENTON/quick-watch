using Domain.User.Models;
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
        if (_context.WorksOn.Any(w => w.UserId == entity.UserId && w.CommissionId == entity.CommissionId))
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

    public Task<List<User>> GetCommissionEmployeesAsync(Guid commissionId)
    {
        var jobs = _context.WorksOn.Where(booking => booking.CommissionId == commissionId);
        var userIds = jobs.Select(job => job.UserId);
        var users = new List<User>();
        foreach (var userId in userIds)
        {
            users.Add(_identityContext.Users.First(user => user.Id == userId));
        }
        return Task.FromResult(users);
    }
}