using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record BaseCommissionDTO
{
    public ClientIdDTO Client { get; init; }
    public CompanyIdDTO Company { get; init; }
    public string Location { get; init; }
    public string Description { get; init; }
    public string Scope { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
};