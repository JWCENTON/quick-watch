namespace DAL.Company.InMemoryAccess;

public class InMemoryCompanyDao : ICompanyDao
{
    private HashSet<Domain.Company.Models.Company> _companies { get; set; }

    public InMemoryCompanyDao()
    {
        _companies = new HashSet<Domain.Company.Models.Company>();
    }

    public void SeedData()
    {
        throw new NotImplementedException();
    }

    public List<Domain.Company.Models.Company> GetAll()
    {
        throw new NotImplementedException();
    }

    public Domain.Company.Models.Company Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Create(Domain.Company.Models.Company entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Domain.Company.Models.Company entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}