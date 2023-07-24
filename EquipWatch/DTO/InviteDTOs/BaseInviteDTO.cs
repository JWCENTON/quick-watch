using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record BaseInviteDTO
{
    public CompanyIdDTO Company { get; init; }
    public UserIdDTO UserId { get; init; }
    public Status Status { get; init; }
    public DateTime CreatedAt { get; init; }
};