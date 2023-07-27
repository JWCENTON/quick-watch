using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record PartialCommissionDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Scope { get; set; } 
    public string Location { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}