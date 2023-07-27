using DAL.Repositories;

namespace DAL.Repositories.Company;

public interface ICompanyRepository : IBaseRepository<Domain.Company.Models.Company>
{
    Task<Domain.Company.Models.Company> GetAsync();
}