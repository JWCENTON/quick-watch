using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record CreateCheckOutDTO
{
    public PartialEquipmentDTO Equipment { get; set; }
    public PartialUserDTO User { get; set; }
    public DateTime Time { get; set; }
}