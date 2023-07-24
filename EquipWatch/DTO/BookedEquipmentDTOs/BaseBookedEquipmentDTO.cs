using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record BaseBookedEquipmentDTO()
{
    public EquipmentIdDTO Equipment { get; init; }
    public CommissionIdDTO Commission { get; init; }
};