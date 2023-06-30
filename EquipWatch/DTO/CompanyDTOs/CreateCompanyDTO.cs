namespace DTO.CompanyDTOs;

public class CreateCompanyDTO
{
    public string Name { get; set; }
    public Domain.User.Models.User Owner { get; set; }
}