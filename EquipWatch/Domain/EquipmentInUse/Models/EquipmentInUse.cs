namespace Domain.EquipmentInUse.Models;
public class EquipmentInUse
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? EndTime { get; set; }
    public Equipment.Models.Equipment Equipment { get; set; }
}

