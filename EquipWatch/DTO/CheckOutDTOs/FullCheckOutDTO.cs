using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record FullCheckOutDTO
{
    public Guid Id { get; set; }
    public FullEquipmentDTO Equipment { get; set; }
    public FullUserDTO User { get; set; }
    public DateTime Time { get; set; }
}