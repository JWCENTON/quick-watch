namespace DTO.EquipmentDTOs;

public record FullEquipmentDTO : BaseEquipmentDTO
{
    public string Id { get; init; }
    public string CompanyId { get; init; }
    public bool Available { get; init; }
    public bool InWarehouse { get; init; }
}