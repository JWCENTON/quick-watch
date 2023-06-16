namespace Domain.Company.Models;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public User.Models.User Owner { get; set; }
}