using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record UpdateClientDTO : BaseClientDTO
{
    public CompanyIdDTO? Company { get; init; }
}