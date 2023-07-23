using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record UpdateInviteDTO
{
    public Guid Id { get; init; }
    public CompanyIdDTO? Company { get; init; }
    public UserIdDTO? UserId { get; init; }
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
}