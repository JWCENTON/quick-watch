using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record UpdateInviteDTO
{
    public PartialCompanyDTO Company { get; set; }
    public PartialUserDTO User { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}