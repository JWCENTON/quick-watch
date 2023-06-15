namespace Domain.Equipment;

public class Equipment
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    //public Company.Models.Company Company { get; set; }
    public bool IsCheckedOut { get; set; }
    //public Employee.Models.Employee? CheckedOutBy { get; set; }

    public Equipment(Guid id, string serialNumber, string category, string location, int condition, bool isCheckedOut)
    {
        Id = id;
        SerialNumber = serialNumber;
        Category = category;
        Location = location;
        Condition = condition;
        IsCheckedOut = isCheckedOut;
    }
}