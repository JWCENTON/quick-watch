using DTO.EmployDTOs;

namespace DTO.CheckInDTOs;

public record BaseCheckInDTO
{
    public EmployIdDTO Employ { get; init; }
    public DateTime Time { get; init; }
};