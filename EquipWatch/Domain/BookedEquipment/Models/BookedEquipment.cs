namespace Domain.BookedEquipment.Models;

public class BookedEquipment
{
    public Equipment.Models.Equipment Equipment { get; set; }
    public Booking.Models.Booking.Booking Booking { get; set; }
}