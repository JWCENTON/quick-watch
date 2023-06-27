namespace Domain.CheckInDTO.DTO;

public class CreateCheckInDTO
{
    public Equipment.Models.Equipment Equipment { get; set; }
    public User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}