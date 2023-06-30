namespace DTO.EquipmentDTOs;

public class UpdateEquipmentDTO
{
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    public Domain.Company.Models.Company Company { get; set; }
    public bool IsCheckedOut { get; set; }
    public Domain.User.Models.User? CheckedOutBy { get; set; }


}