namespace Domain.Reservation.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public Equipment.Models.Equipment Equipment { get; set; }
}