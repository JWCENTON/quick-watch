using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record CreateCompanyDTO
{
    public string Name { get; set; }
    public UserIdDTO Owner { get; set; }
}