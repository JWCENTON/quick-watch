namespace DTO.EmployDTOs;

public record FullEmployDTO : BaseEmployDTO
{
    public Guid Id { get; init; }
}