namespace Domain.Client.DTO;

public class ClientDto
{
    public Company.Models.Company Company { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}