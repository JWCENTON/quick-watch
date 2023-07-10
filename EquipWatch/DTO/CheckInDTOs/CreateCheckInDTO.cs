using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record CreateCheckInDTO
{
    public EquipmentIdDTO Equipment { get; set; }
    public UserIdDTO User { get; set; }
    public DateTime Time { get; set; }
}
