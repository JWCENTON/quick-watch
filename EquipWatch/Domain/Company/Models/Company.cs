namespace Domain.Company.Models;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
}