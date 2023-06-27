namespace Domain.Company.DTO;

public class CreateCompanyDTO
{
    public string Name { get; set; }
    public User.Models.User Owner { get; set; }
}