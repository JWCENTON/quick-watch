namespace Domain.BookedEquipment.Models;

public class BookedEquipment
{
    public Guid Id { get; set; }
    public Guid CommissionId { get; set; }
    //public Guid? CheckOutId { get; set; }
    //public Guid? CheckInId { get; set; }
    public Guid? ReservationId { get; set; }
    public Guid? EquipmentInUseId { get; set; }
    public bool IsFinished { get; set; }
    public Commission.Models.Commission.Commission Commission { get; set; }
    public Reservation.Models.Reservation? Reservation { get; set;}
    //public CheckOut.Models.CheckOut? CheckOut { get; set; }
    //public CheckIn.Models.CheckIn ? CheckIn { get; set; }
    public EquipmentInUse.Models.EquipmentInUse EquipmentInUse { get; set; }
}