using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record CreateEmployDTO
{
    public CompanyIdDTO Company { get; set; }
    public UserIdDTO UserId { get; set; }
    public Role Role { get; set; }
}