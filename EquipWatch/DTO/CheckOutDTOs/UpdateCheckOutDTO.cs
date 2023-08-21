namespace DTO.CheckOutDTOs;

public record UpdateCheckOutDTO
{
    public string? UserId { get; init; }
    public string? EquipmentId { get; init; }
}