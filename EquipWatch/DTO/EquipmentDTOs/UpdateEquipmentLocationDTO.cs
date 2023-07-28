using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record UpdateEquipmentLocationDTO()
{
    public string? Location { get; set; }
}