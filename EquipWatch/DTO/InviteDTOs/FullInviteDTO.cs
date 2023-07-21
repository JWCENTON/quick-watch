namespace DTO.InviteDTOs;

public record FullInviteDTO : BaseInviteDTO
{
    public Guid Id { get; init; }
}