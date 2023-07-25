using DTO.EmployDTOs;
using DTO.EquipmentDTOs;

namespace DTO.CheckOutDTOs;

public record BaseCheckOutDTO
{
    public string EmployId { get; init; }
    public string EquipmentId { get; init; }
    public DateTime Time { get; init; }
};