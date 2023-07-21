namespace DTO.EquipmentDTOs;

public record BaseEquipmentDTO
{
    public string? Category { get; init; }
    public string? Location { get; init; }
    public int? Condition { get; init; }
};