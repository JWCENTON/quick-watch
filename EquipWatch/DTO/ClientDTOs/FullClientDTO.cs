using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record FullClientDTO : BaseClientDTO
{
    public string Id { get; init; }
    public string CompanyId { get; init; }
}