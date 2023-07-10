using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record UpdateCheckOutDTO
{
    public UserIdDTO User { get; set; }
    public DateTime Time { get; set; }
}