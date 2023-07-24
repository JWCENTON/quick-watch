using DTO.EmployDTOs;
using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record UpdateCheckInDTO
{
    public EmployIdDTO? Employ { get; init; }
    public EquipmentIdDTO? Equipment { get; init; }
    public DateTime? Time { get; init; }
}