using DTO.CommissionDTOs;
using DTO.EmployDTOs;

namespace DTO.WorksOnDTOs;

public record UpdateWorksOnDTO()
{
    public string? CommissionId { get; init; }
    public string? EmployeeId { get; init; }
};