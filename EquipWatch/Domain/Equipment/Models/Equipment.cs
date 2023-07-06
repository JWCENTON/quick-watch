namespace Domain.Equipment.Models;

public class Equipment
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; } // mb enum
    public Company.Models.Company Company { get; set; }
    public Commission.Models.Commission.Commission? Commission { get; set; } // 
    public bool IsCheckedOut { get; set; }
    public User.Models.User? CheckedOutBy { get; set; }

    //status - available / reserved, in use
}