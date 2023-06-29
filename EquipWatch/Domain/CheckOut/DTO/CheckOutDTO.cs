namespace Domain.CheckOut.DTO;

public class CheckOutDTO
{
    public Equipment.Models.Equipment Equipment { get; set; }
    public User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}