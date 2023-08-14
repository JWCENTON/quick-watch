namespace DTO.CheckOutDTOs;

public record BaseCheckOutDTO
{
    public string UserId { get; init; }
    public string EquipmentId { get; init; }
    public DateTime ArriveTime { get; init; }
    public DateTime EndTime { get; init; }
};