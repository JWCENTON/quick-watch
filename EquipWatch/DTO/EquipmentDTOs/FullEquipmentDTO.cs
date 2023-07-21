using DTO.CompanyDTOs;

namespace DTO.EquipmentDTOs;

public record FullEquipmentDTO : BaseEquipmentDTO
{
    public Guid Id { get; init; }
    public string SerialNumber { get; init; }
    public CompanyIdDTO? Company { get; init; }
    public bool IsCheckedOut { get; init; }
}