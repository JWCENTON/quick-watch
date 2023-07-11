using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record UpdateEquipmentDTO
{
    //public string Name { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    public CompanyIdDTO Company { get; set; }
    public bool IsCheckedOut { get; set; }
    public UserIdDTO? CheckedOutBy { get; set; }


}