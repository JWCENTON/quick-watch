namespace Domain.WorksOn.Models;

public class WorksOn
{
    public Booking.Models.Booking.Booking Booking { get; set; }
    public User.Models.User User { get; set; }
}