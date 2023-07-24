using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record UpdateEquipmentLocationDTO()
{
    public string Location { get; set; }
    public UserIdDTO UserId { get; set; }
}