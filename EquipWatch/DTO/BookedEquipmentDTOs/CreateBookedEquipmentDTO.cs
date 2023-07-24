using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record CreateBookedEquipmentDTO() : BaseBookedEquipmentDTO
{
    public Guid Id { get; init; }
};