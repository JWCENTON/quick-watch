using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record UpdateCheckInDTO
{
    public UserIdDTO User { get; set; }
    public DateTime Time { get; set; }
}