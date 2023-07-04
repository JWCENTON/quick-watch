using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record CreateInviteDTO
{
    public FullCompanyDTO Company { get; set; }
    public FullUserDTO User { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}