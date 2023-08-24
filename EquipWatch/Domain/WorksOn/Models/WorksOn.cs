namespace Domain.WorksOn.Models;

public class WorksOn
{
    public Guid Id { get; set; }
    public Guid CommissionId { get; set; }
    public string UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EndTime { get; set; }
    public Commission.Models.Commission.Commission Commission { get; set; }
}