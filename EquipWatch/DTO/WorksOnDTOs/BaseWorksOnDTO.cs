using DTO.CommissionDTOs;
using DTO.EmployDTOs;

namespace DTO.WorksOnDTOs;

public record BaseWorksOnDTO()
{
    public CommissionIdDTO Commission { get; init; }
    public EmployIdDTO Employee { get; init; }
};