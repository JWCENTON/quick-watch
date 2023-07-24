using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record UpdateCommissionDTO
{
    public string? CompanyId { get; init; }
    public string? ClientId { get; init; }
    public string? Location { get; init; }
    public string? Description { get; init; }
    public string? Scope { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
}