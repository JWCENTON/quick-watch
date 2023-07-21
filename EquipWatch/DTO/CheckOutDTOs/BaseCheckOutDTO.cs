using DTO.EmployDTOs;

namespace DTO.CheckOutDTOs;

public record BaseCheckOutDTO
{
    public EmployIdDTO Employ { get; init; }
    public DateTime Time { get; init; }
};