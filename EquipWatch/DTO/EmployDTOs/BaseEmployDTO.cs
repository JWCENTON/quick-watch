using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record BaseEmployDTO
{
    public CompanyIdDTO Company { get; init; }
    public UserIdDTO UserId { get; init; }
    public Role Role { get; init; }
};