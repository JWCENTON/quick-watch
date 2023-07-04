using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record FullCommissionDTO
{
    public Guid Id { get; set; }
    public FullCompanyDTO Company { get; set; }
    public FullClientDTO Client { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}