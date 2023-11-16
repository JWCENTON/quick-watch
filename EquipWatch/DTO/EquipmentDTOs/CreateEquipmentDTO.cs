namespace DTO.EquipmentDTOs;

public record CreateEquipmentDTO : BaseEquipmentDTO
{
    public string CompanyId { get; init; }
}