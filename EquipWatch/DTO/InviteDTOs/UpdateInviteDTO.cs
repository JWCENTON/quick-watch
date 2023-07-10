using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record UpdateInviteDTO
{
    public CompanyIdDTO Company { get; set; }
    public UserIdDTO User { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}