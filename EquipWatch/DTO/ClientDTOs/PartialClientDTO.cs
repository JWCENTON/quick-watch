namespace DTO.ClientDTOs;

public record PartialClientDTO : BaseClientDTO
{
    public Guid Id { get; init; }
}