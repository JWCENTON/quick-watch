using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record UpdateCheckInDTO
{
    public PartialUserDTO User { get; set; }
    public DateTime Time { get; set; }
}