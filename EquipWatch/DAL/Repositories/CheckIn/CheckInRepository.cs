using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.CheckIn;

public class CheckInRepository : ICheckInRepository
{
    private readonly DatabaseContext _context;

    public CheckInRepository(DatabaseContext context)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Domain.CheckIn.Models.CheckIn>> GetAllAsync()
    {
        return await _context.CheckIns.ToListAsync();
    }

    public async Task<Domain.CheckIn.Models.CheckIn> GetAsync(Guid id)
    {
        var client = await _context.CheckIns.FirstOrDefaultAsync(c => c.Id == id);
        return client ?? throw new KeyNotFoundException("CheckIn With given Id was not found");
    }

    public async Task CreateAsync(Domain.CheckIn.Models.CheckIn entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.CheckIn.Models.CheckIn entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}