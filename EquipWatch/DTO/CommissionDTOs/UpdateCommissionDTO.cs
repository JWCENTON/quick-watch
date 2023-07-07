using DTO.ClientDTOs;

namespace DTO.CommissionDTOs;

public record UpdateCommissionDTO
{
    public PartialClientDTO Client { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Scope { get; set; } 
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}