using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record CreateBookedEquipmentDTO()
{
    public string? EquipmentId { get; init; }
    public string? CommissionId { get; init; }
    public DateTime? EndTime { get; init; }
};