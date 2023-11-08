namespace DTO.BookedEquipmentDTOs;

public record FullBookedEquipmentDTO() : BaseBookedEquipmentDTO
{
    public string Id { get; init; }
};