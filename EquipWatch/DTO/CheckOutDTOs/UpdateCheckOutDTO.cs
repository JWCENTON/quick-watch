using DTO.UserDTOs;

namespace DTO.CheckOutDTOs;

public record UpdateCheckOutDTO
{
    public PartialUserDTO User { get; set; }
    public DateTime Time { get; set; }
}