namespace Domain.Equipment.Models;

public class Equipment
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; } // mb enum
    public Guid CompanyId { get; set; }
    public bool Available { get; set; }
    public bool InWarehouse { get; set; }
    public Company.Models.Company Company { get; set; }
}