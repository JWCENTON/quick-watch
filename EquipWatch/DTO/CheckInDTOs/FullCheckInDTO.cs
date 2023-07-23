using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record FullCheckInDTO : BaseCheckInDTO
{
    public Guid Id { get; init; }
}