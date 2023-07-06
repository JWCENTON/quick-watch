namespace Domain.Employee.Models;

public class Employee
{
    public Guid Id { get; set; }
    public Company.Models.Company Company { get; set; }
    public User.Models.User User { get; set; }
    public Role Role { get; set; }
}