using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record CreateClientDTO : BaseClientDTO
{
    public CompanyIdDTO Company { get; init; }
}