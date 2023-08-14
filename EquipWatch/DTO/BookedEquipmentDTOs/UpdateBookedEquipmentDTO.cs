using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record UpdateBookedEquipmentDTO()
{
    public string? CheckOutId { get; init; }
    public string? EquipmentId { get; init; }
    public string? CommissionId { get; init; }
};