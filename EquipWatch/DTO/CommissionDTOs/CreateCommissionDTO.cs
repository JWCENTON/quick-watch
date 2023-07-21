using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record CreateCommissionDTO : BaseCommissionDTO
{
    public CompanyIdDTO Company { get; init; }
}