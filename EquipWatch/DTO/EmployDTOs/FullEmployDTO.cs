using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record FullEmployDTO : BaseEmployDTO
{
    public Guid Id { get; init; }
}