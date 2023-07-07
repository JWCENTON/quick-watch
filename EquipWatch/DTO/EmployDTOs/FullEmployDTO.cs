using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record FullEmployDTO
{
    public FullCompanyDTO Company { get; set; }
    public FullUserDTO User { get; set; }
    public Role Role { get; set; }
}