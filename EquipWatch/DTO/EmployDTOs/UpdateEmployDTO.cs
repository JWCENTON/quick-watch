using Domain.Employ;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record UpdateEmployDTO
{
    public FullCompanyDTO Company { get; set; }
    public FullUserDTO User { get; set; }
    public Role Role { get; set; }
}