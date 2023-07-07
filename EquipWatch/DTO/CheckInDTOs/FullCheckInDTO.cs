using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record FullCheckInDTO
{
    public Guid Id { get; set; }
    public FullEquipmentDTO Equipment { get; set; }
    public FullUserDTO User { get; set; }
    public DateTime Time { get; set; }
}