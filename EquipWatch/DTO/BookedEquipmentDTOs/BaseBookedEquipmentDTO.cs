namespace DTO.BookedEquipmentDTOs;

public record BaseBookedEquipmentDTO()
{
    public string? CheckOutId { get; init; }
    public string CommissionId { get; init; }
    public string? ReservationId { get; init; }
};