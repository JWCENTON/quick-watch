namespace Domain.CheckOut.Models;

public class CheckOut
{
    public Guid Id { get; set; }
    public Equipment.Models.Equipment Equipment { get; set; }
    public Employee.Models.Employee Employee { get; set; }
    public DateTime Time { get; set; }
}