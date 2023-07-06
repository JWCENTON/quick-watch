using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record CreateCheckInDTO
{
    public PartialEquipmentDTO Equipment { get; set; }
    public PartialUserDTO User { get; set; }
    public DateTime Time { get; set; }
}
