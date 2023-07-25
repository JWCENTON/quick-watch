using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record BaseBookedEquipmentDTO()
{
    public string EquipmentId { get; init; }
    public string CommissionId { get; init; }
};