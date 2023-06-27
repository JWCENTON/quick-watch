namespace Domain.CheckInDTO.DTO;

public class UpdateCheckInDTO
{
    public User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}