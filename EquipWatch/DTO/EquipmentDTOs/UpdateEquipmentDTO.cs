namespace DTO.EquipmentDTOs;

public record UpdateEquipmentDTO
{
    public string? CompanyId { get; init; }
    public string? Category { get; init; }
    public string? SerialNumber { get; init; }
    public string? Location { get; init; }
    public int? Condition { get; init; }

}