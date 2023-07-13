using DTO.CompanyDTOs;

namespace DTO.EquipmentDTOs;

public record CreateEquipmentDTO
{
    public string SerialNumber { get; init; }
    //public string Name { get; set; }
    public string Category { get; init; }
    public string Location { get; init; }
    public int Condition { get; init; }
    public CompanyIdDTO? Company { get; init; }
}