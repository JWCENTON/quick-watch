using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public class UpdateCheckInDTO
{
    public CreateUserDTO User { get; set; }
    public DateTime Time { get; set; }
}