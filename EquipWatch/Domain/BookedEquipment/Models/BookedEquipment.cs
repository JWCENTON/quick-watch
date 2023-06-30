namespace Domain.BookedEquipment.Models;

public class BookedEquipment
{
    public Equipment.Models.Equipment Equipment { get; set; }
    public Commission.Models.Commission.Commission Booking { get; set; }
}