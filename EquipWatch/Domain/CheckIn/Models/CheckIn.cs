namespace Domain.CheckIn.Models;

public class CheckIn
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string UserId { get; set; }
    public bool WarehouseDelivery { get; set; }
    public DateTime CreationTime { get; set; }
    public Equipment.Models.Equipment Equipment { get; set; }
}