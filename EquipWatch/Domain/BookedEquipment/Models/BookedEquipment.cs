namespace Domain.BookedEquipment.Models;

public class BookedEquipment
{
    public Guid Id { get; set; } // or database generated int
    public Equipment.Models.Equipment Equipment { get; set; }
    public Commission.Models.Commission.Commission Commission { get; set; }
}