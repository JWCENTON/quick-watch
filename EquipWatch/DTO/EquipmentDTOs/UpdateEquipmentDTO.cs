using DTO.CompanyDTOs;

namespace DTO.EquipmentDTOs;

public record UpdateEquipmentDTO : BaseEquipmentDTO
{
    public CompanyIdDTO? Company { get; init; }
    public bool? IsCheckedOut { get; init; }

}