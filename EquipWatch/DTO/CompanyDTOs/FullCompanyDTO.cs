namespace DTO.Company;

public class FullCompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Domain.User.Models.User Owner { get; set; }
}