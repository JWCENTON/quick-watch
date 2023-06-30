namespace DTO.CompanyDTOs;

public class UpdateCompanyDTO
{
    public string Name { get; set; }
    public Domain.User.Models.User Owner { get; set; }
}