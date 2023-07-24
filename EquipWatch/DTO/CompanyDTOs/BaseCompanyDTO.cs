using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record BaseCompanyDTO
{
    public string Name { get; init; }
    public UserIdDTO OwnerId { get; init; }
}