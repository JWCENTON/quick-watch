using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record FullCompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public FullUserDTO Owner { get; set; }
}