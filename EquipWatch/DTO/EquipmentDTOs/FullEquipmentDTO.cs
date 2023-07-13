using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record FullEquipmentDTO
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    //public string Name { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    public FullCompanyDTO Company { get; set; }
    public bool IsCheckedOut { get; set; }
}