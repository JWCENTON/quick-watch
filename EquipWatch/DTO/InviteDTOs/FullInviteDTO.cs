using Domain.Invite;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.InviteDTOs;

public record FullInviteDTO
{
    public Guid Id { get; set; }
    public FullCompanyDTO Company { get; set; }
    public FullUserDTO UserId { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}