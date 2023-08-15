namespace DTO.EquipmentDTOs;

public record PartialEquipmentDTO : BaseEquipmentDTO
{
    public string Id { get; init; }
    public bool Available { get; init; }
    public bool InWarehouse { get; init; }
}