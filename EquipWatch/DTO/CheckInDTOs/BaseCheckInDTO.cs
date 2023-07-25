using DTO.EmployDTOs;
using DTO.EquipmentDTOs;

namespace DTO.CheckInDTOs;

public record BaseCheckInDTO
{
    public string UserId { get; init; }
    public string EquipmentId { get; init; }
    public DateTime Time { get; init; }
};