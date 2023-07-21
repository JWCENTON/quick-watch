using DTO.EquipmentDTOs;

namespace DTO.CheckOutDTOs;

public record CreateCheckOutDTO : BaseCheckOutDTO
{
    public EquipmentIdDTO Equipment { get; init; }
}