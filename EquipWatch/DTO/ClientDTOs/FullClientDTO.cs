using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record FullClientDTO : BaseClientDTO
{
    public Guid Id { get; init; }
    public CompanyIdDTO Company { get; init; }
}