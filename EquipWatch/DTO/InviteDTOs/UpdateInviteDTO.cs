using Domain.Invite;

namespace DTO.InviteDTOs;

public class UpdateInviteDTO
{
    public Domain.Company.Models.Company Company { get; set; }
    public Domain.User.Models.User User { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}