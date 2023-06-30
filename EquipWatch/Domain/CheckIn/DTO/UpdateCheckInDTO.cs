namespace Domain.CheckIn.DTO;

public class UpdateCheckInDTO
{
    public User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}