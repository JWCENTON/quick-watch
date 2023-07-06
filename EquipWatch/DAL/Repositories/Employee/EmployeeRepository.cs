using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Employee;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DatabaseContext _context;

    public EmployeeRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Domain.Employee.Models.Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Domain.Employee.Models.Employee> GetAsync(Guid id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
        return employee ?? throw new KeyNotFoundException("Employee with given Id was not found");
    }

    public async Task CreateAsync(Domain.Client.Models.Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.Client.Models.Client entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}