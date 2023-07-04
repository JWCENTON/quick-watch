using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record CreateCheckInDTO
{
    public FullEquipmentDTO Equipment { get; set; }
    public FullUserDTO User { get; set; }
    public DateTime Time { get; set; }
}
