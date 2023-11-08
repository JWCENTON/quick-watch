namespace Domain.Client.Models;

public class Client
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Company.Models.Company Company { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ContactAddress { get; set; }
}