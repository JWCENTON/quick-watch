using DTO.CommissionDTOs;
using DTO.EmployDTOs;

namespace DTO.WorksOnDTOs;

public record BaseWorksOnDTO()
{
    public string CommissionId { get; init; }
    public string UserId { get; init; }
};