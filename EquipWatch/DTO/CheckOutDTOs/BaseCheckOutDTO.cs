using DTO.EmployDTOs;
using DTO.EquipmentDTOs;

namespace DTO.CheckOutDTOs;

public record BaseCheckOutDTO
{
    public EmployIdDTO Employ { get; init; }
    public EquipmentIdDTO Equipment { get; init; }
    public DateTime Time { get; init; }
};