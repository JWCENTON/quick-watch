namespace DTO.EquipmentDTOs;

public record PartialEquipmentDTO : BaseEquipmentDTO
{
    public string Id { get; init; }
    public string SerialNumber { get; init; }
    public bool IsCheckedOut { get; init; }
}