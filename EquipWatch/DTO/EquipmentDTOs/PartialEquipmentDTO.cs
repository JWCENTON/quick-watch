namespace DTO.EquipmentDTOs;

public record PartialEquipmentDTO : BaseEquipmentDTO
{
    public Guid Id { get; init; }
    public string SerialNumber { get; init; }
    public bool IsCheckedOut { get; init; }
}