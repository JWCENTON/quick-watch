using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record FullClientDTO
{
    public Guid Id { get; set; }
    public PartialCompanyDTO Company { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ContactAddress { get; set; }
}