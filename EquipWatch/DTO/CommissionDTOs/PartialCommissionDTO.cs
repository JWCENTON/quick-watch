using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record PartialCommissionDTO
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public string Scope { get; init; } 
    public string Location { get; init; }
    public string StartTime { get; init; }
    public string? EndTime { get; init; }
}