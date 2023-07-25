using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record UpdateInviteDTO
{
    public string? CompanyId { get; init; }
    public string? UserId { get; init; }
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
}