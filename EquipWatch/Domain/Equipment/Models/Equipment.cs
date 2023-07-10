namespace Domain.Equipment.Models;

public class Equipment
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; } // mb enum
    public Company.Models.Company Company { get; set; }
    public Commission.Models.Commission.Commission? Commission { get; set; } 
    // does it have sense while we already have BookedEquipment that connect commission with equipment already
    public bool IsCheckedOut { get; set; }
    public Employee.Models.Employee? CheckedOutBy { get; set; }

    //status - available / reserved, in use
}