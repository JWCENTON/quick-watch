using DTO.ClientDTOs;

namespace DTO.CommissionDTOs;

public record UpdateCommissionDTO
{
    public FullClientDTO Client { get; set; }
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}