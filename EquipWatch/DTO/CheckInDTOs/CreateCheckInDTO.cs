using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record CreateCheckInDTO : BaseCheckInDTO
{
    public EquipmentIdDTO Equipment { get; init; }
}
