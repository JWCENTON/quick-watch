namespace DTO.EquipmentDTOs;

public record PartialEquipmentDTO : BaseEquipmentDTO
{
    public string Id { get; init; }
    public bool Available { get; init; }
}