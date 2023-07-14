using DTO.UserDTOs;

namespace DTO.CompanyDTOs;

public record PartialCompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}