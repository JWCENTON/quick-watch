namespace DTO.CheckIn;

public class FullCheckInDTO
{
    public Guid Id { get; set; }
    public Domain.Equipment.Models.Equipment Equipment { get; set; }
    public Domain.User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}