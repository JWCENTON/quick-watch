using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record FullCheckInDTO : BaseCheckInDTO
{
    public string Id { get; init; }
}