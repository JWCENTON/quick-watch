using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record CreateCheckOutDTO
{
    public EquipmentIdDTO Equipment { get; set; }
    public UserIdDTO User { get; set; }
    public DateTime Time { get; set; }
}