namespace DTO.BookedEquipmentDTOs;

public record BaseBookedEquipmentDTO()
{
    public string? EquipmentInUseId { get; init; }
    public string CommissionId { get; init; }
    public string? ReservationId { get; init; }
    public bool IsFinished { get; init; }
};