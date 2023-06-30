namespace DTO.CheckInDTOs;

public class UpdateCheckInDTO
{
    public Domain.User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}