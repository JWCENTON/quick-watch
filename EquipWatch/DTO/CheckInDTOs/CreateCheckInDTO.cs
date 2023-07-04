using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public class CreateCheckInDTO
{
    public CreateEquipmentDTO Equipment { get; set; }
    public CreateUserDTO User { get; set; }
    public DateTime Time { get; set; }
}