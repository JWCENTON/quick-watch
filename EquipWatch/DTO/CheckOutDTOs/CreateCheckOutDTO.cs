using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record CreateCheckOutDTO
{
    public FullEquipmentDTO Equipment { get; set; }
    public FullUserDTO User { get; set; }
    public DateTime Time { get; set; }
}