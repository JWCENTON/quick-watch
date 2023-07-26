using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record PartialCommissionDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Scope { get; set; } 
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}