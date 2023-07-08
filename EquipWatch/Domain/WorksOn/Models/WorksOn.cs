namespace Domain.WorksOn.Models;

public class WorksOn
{
    public Guid Id { get; set; } // or database generated int
    public Commission.Models.Commission.Commission Commission { get; set; }
    public Employee.Models.Employee Employee { get; set; }
}