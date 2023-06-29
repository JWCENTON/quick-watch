namespace Domain.Employ.DTO;

public class CreateEmployDTO
{
    public Company.Models.Company Company { get; set; }
    public User.Models.User User { get; set; }
    public Role Role { get; set; }
}