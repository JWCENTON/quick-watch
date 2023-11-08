namespace DTO.ClientDTOs;

public record CreateClientDTO : BaseClientDTO
{
    public string CompanyId { get; init; }
}