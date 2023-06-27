using webapi.Entities.BaseServices;

namespace webapi.Entities.CompanyApi.Services;

public interface ICompanyService : IBaseService<Domain.Company.Models.Company>
{
    public Task<Domain.Company.Models.Company> GetCompany(Guid id);
}