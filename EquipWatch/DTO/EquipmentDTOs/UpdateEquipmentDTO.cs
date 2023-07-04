using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record UpdateEquipmentDTO
{
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    public FullCompanyDTO Company { get; set; }
    public bool IsCheckedOut { get; set; }
    public FullUserDTO? CheckedOutBy { get; set; }


}