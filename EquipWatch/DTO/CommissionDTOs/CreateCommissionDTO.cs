using DTO.ClientDTOs;
using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record CreateCommissionDTO
{
    public PartialCompanyDTO Company { get; set; }
    public PartialClientDTO Client { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Scope { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}