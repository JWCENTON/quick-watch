using DTO.EmployDTOs;
using DTO.EquipmentDTOs;

namespace DTO.CheckInDTOs;

public record BaseCheckInDTO
{
    public EmployIdDTO Employ { get; init; }
    public EquipmentIdDTO Equipment { get; init; }
    public DateTime Time { get; init; }
};