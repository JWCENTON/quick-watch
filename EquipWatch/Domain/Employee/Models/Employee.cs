namespace Domain.Employee.Models;

public class Employee
{
    public Guid Id { get; set; }
    public Company.Models.Company Company { get; set; }
    public string UserId { get; set; }
    public Role Role { get; set; }
}