using DTO.CommissionDTOs;
using DTO.EmployDTOs;

namespace DTO.WorksOnDTOs;

public record UpdateWorksOnDTO()
{
    public CommissionIdDTO? Commission { get; init; }
    public EmployIdDTO? Employee { get; init; }
};