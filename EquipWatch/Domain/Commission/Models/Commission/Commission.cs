namespace Domain.Commission.Models.Commission;

public class Commission
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid ClientId { get; set; }
    public Company.Models.Company Company { get; set; }
    public Client.Models.Client Client { get; set; }
    public string Description { get; set; }
    public string Scope { get; set; }
    public string Location { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set;}

    //public List<Employee.Models.Employee>? Employees { get; set; } // how to save it to DB to contain only ID
    // list of photos of current work
}