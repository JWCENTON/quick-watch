using DAL;
using Domain.Company.Models;


namespace webapi.Entities.CompanyApi.Services;

public class CompanyService : ICompanyService
{
    private readonly DatabaseContext _context;

    public CompanyService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Company> GetCompany(Guid id)
    {
        return await _context.Company.FindAsync(id);
    }
    
    public void SeedData()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Company>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Company> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Create(Company entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Company entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}