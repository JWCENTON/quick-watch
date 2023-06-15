namespace webapi.Entities.Company.Services;

public interface ICompanyService
{
    public Domain.Company.Models.Company GetCompany(Guid id);
}