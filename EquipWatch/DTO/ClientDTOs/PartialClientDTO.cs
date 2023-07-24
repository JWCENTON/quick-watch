namespace DTO.ClientDTOs;

public record PartialClientDTO : BaseClientDTO
{
    public string Id { get; init; }
}