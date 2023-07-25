using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record UpdateClientDTO
{
    public string? CompanyId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? ContactAddress { get; init; }
}