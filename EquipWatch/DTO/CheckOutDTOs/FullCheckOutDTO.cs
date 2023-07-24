using DTO.EquipmentDTOs;

namespace DTO.CheckOutDTOs;

public record FullCheckOutDTO : BaseCheckOutDTO
{
    public string Id { get; init; }
}