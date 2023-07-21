using DTO.EquipmentDTOs;

namespace DTO.CheckOutDTOs;

public record FullCheckOutDTO : BaseCheckOutDTO
{
    public Guid Id { get; init; }
    public FullEquipmentDTO Equipment { get; init; }
}