namespace Domain.WorksOn.Models;

public class WorksOn
{
    public Commission.Models.Commission.Commission Booking { get; set; }
    public User.Models.User User { get; set; }
}