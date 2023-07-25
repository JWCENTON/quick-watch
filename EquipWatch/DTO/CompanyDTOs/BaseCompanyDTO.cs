using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record BaseCompanyDTO
{
    public string Name { get; init; }
    public string OwnerId { get; init; }
}