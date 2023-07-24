using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record UpdateBookedEquipmentDTO()
{
    public EquipmentIdDTO? Equipment { get; init; }
    public CommissionIdDTO? Commission { get; init; }
};