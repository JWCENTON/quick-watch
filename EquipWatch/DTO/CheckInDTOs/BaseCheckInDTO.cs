namespace DTO.CheckInDTOs;

public record BaseCheckInDTO
{
    public string UserId { get; init; }
    public string EquipmentId { get; init; }
    public DateTime ArriveTime { get; init; }
};