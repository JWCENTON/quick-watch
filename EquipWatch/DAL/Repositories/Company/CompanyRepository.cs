namespace DAL.Repositories.Company;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _context;

    public CompanyRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<Domain.Company.Models.Company> GetCompanyAsync(Guid id)
    {
        return await _context.Company.FindAsync(id);
    }

    public async Task<List<Domain.Company.Models.Company>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Company.Models.Company> GetAsync(Guid id)
    {
        throw new NotImplementedException();
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