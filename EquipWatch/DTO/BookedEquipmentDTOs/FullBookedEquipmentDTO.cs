using DTO.CommissionDTOs;
using DTO.EquipmentDTOs;

namespace DTO.BookedEquipmentDTOs;

public record FullBookedEquipmentDTO() : BaseBookedEquipmentDTO
{
    public Guid Id { get; init; }
};