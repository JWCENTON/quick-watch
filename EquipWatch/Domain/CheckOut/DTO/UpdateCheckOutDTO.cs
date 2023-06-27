namespace Domain.CheckOut.DTO;

public class UpdateCheckOutDTO
{
    public User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}