using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record UpdateCheckOutDTO
{
    public FullUserDTO User { get; set; }
    public DateTime Time { get; set; }
}