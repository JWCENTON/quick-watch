using DTO.CompanyDTOs;

namespace DTO.EquipmentDTOs;

public record UpdateEquipmentDTO
{
    public CompanyIdDTO? Company { get; init; }
    public bool? IsCheckedOut { get; init; }
    public string? Category { get; init; }
    public string? SerialNumber { get; init; }
    public string? Location { get; init; }
    public int? Condition { get; init; }

}