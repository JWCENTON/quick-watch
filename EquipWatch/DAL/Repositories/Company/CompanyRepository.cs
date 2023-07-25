using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Company;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _context;

    public CompanyRepository(DatabaseContext context)
    {
        _context = context;
        Seed.IfDbEmptyAddNewItems(context);
    }

    public async Task<List<Domain.Company.Models.Company>> GetAllAsync()
    {
        return await _context.Company.ToListAsync();
    }

    public async Task<Domain.Company.Models.Company> GetAsync(Guid id)
    {
        var company = await _context.Company.FirstOrDefaultAsync(company => company.Id == id);
        return company ?? throw new KeyNotFoundException("Company With given Id was not found");
    }

    public async Task CreateAsync(Domain.Company.Models.Company entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Domain.Company.Models.Company entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}