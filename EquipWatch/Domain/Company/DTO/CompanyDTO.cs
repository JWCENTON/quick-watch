namespace Domain.Company.DTO;

public class CompanyDto
{
    public string Name { get; set; }
    public User.Models.User Owner { get; set; }
}