using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record CreateInviteDTO
{
    public CompanyIdDTO Company { get; set; }
    public UserIdDTO UserId { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}