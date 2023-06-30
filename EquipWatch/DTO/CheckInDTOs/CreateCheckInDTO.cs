namespace DTO.CheckInDTOs;

public class CreateCheckInDTO
{
    public Domain.Equipment.Models.Equipment Equipment { get; set; }
    public Domain.User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}