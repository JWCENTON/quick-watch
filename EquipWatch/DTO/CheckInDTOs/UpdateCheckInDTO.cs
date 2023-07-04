using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record UpdateCheckInDTO
{
    public FullUserDTO User { get; set; }
    public DateTime Time { get; set; }
}