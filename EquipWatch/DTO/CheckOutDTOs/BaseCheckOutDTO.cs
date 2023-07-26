namespace DTO.CheckOutDTOs;

public record BaseCheckOutDTO
{
    public string UserId { get; init; }
    public string EquipmentId { get; init; }
    public DateTime Time { get; init; }
};