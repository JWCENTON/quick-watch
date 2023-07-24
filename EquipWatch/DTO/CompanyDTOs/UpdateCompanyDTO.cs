using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record UpdateCompanyDTO
{
    public string? Name { get; init; }
    public UserIdDTO? OwnerId { get; init; }
}