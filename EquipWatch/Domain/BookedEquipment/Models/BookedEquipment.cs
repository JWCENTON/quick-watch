namespace Domain.BookedEquipment.Models;

public class BookedEquipment
{
    public Guid Id { get; set; }
    public Guid CommissionId { get; set; }
    public Guid? CheckOutId { get; set; }
    public Guid? ReservationId { get; set; }
    public Commission.Models.Commission.Commission Commission { get; set; }
    public CheckOut.Models.CheckOut? CheckOut { get; set; }
    public Reservation.Models.Reservation? Reservation { get; set;}
}