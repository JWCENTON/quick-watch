namespace DTO.CommissionDTOs;

public record FullCommissionDTO : BaseCommissionDTO
{
    public Guid Id { get; init; }
}