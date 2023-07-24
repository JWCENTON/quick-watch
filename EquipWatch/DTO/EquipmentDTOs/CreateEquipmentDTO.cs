using DTO.CompanyDTOs;

namespace DTO.EquipmentDTOs;

public record CreateEquipmentDTO : BaseEquipmentDTO
{
    public string SerialNumber { get; init; }
    public CompanyIdDTO Company { get; init; }
}