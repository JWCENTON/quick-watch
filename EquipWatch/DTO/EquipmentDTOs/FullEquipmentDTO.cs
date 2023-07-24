using DTO.CompanyDTOs;

namespace DTO.EquipmentDTOs;

public record FullEquipmentDTO : BaseEquipmentDTO
{
    public string Id { get; init; }
    public string SerialNumber { get; init; }
    public string CompanyId { get; init; }
    public bool IsCheckedOut { get; init; }
}