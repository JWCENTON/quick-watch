using DTO.CommissionDTOs;
using DTO.EmployDTOs;

namespace DTO.WorksOnDTOs;

public record FullWorksOnDTO() : BaseWorksOnDTO
{
    public Guid Id { get; init; }
};