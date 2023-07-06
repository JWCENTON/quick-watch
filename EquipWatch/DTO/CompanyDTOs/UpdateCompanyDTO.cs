using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record UpdateCompanyDTO
{
    public string Name { get; set; }
    public PartialUserDTO Owner { get; set; }
}