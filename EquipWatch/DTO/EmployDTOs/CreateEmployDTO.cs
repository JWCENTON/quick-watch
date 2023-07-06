using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record CreateEmployDTO
{
    public PartialCompanyDTO Company { get; set; }
    public PartialUserDTO User { get; set; }
    public Role Role { get; set; }
}