using DAL.Company;
using DAL.Equipment;

namespace webapi.Entities.Company.Services;

public class CompanyService : ICompanyService
{
    private ICompanyDao Dao { get; set; }

    public CompanyService(ICompanyDao companyDao)
    {
        Dao = companyDao;
    }

    public Domain.Company.Models.Company GetCompany(Guid id)
    {
        return Dao.Get(id);
    }
}