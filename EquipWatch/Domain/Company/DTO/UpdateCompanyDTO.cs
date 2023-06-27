namespace Domain.Company.DTO;

public class UpdateCompanyDTO
{
    public string Name { get; set; }
    public User.Models.User Owner { get; set; }
}