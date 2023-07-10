using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record UpdateEmployDTO
{
    public CompanyIdDTO Company { get; set; }
    public UserIdDTO User { get; set; }
    public Role Role { get; set; }
}