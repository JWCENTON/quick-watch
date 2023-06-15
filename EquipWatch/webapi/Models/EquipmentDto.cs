namespace webapi.Models
{
    public record CreateEquipmentDto
    {
        public string SerialNumber { get; init; }
    }

    public record UpdateEquipmentDto : CreateEquipmentDto
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public int Condition { get; set; }
        //public Company.Models.Company Company { get; set; }
        public bool IsCheckedOut { get; set; } = false;
    }

    public record EquipmentDto : CreateEquipmentDto
    {
        public string Category { get; set; }
        public string Location { get; set; }
        public int Condition { get; set; }
        //public Company.Models.Company Company { get; set; }
        public bool IsCheckedOut { get; set; } = false;
        //public Employee.Models.Employee? CheckedOutBy { get; set; }
    }
}
