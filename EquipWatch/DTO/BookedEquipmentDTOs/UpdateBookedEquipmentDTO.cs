namespace DTO.BookedEquipmentDTOs;

public record UpdateBookedEquipmentDTO()
{
    public string? EquipmentInUseId { get; init; }
    public string? EquipmentId { get; init; }
    public string? CommissionId { get; init; }
};