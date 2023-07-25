namespace Domain.WorksOn.Models;

public class WorksOn
{
    public Guid Id { get; set; }
    public Guid CommissionId { get; set; }
    public Guid EmployeeId { get; set; }
    public Commission.Models.Commission.Commission Commission { get; set; }
    public Employee.Models.Employee Employee { get; set; }
}