namespace Domain.CommissionedEquipment.Models;

public class CommissionedEquipment
{
    public Equipment.Models.Equipment Equipment { get; set; }
    public Commission.Models.Commission.Commission Booking { get; set; }
}