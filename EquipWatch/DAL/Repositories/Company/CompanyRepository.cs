namespace DAL.Repositories.Company;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _context;

    public CompanyRepository(DatabaseContext context)
    {
        _context = context;
        DatabaseContext.IfDbEmptyAddNewItems(context);
    }

    public async Task<Domain.Company.Models.Company> GetCompany(Guid id)
    {
        return await _context.Company.FindAsync(id);
    }

    public void SeedData()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Domain.Company.Models.Company>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Company.Models.Company> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Create(Domain.Company.Models.Company entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Domain.Company.Models.Company entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}