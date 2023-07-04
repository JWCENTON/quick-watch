using DAL.Repositories;

namespace DAL.Repositories.Company;

public interface ICompanyRepository : IBaseRepository<Domain.Company.Models.Company>
{
    public Task<Domain.Company.Models.Company> GetCompanyAsync(Guid id);
}