using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.User.Models.User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.User.Models.User> GetAsync(Guid id)
    {
        //var employee = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        //return employee ?? throw new KeyNotFoundException("Employee with given Id was not found");
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Domain.User.Models.User entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.User.Models.User entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}