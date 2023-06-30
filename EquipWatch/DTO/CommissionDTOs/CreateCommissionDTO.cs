namespace DTO.CommissionDTOs;

public class CreateCommissionDTO
{
    public Domain.Company.Models.Company Company { get; set; }
    public Domain.Client.Models.Client Client { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}