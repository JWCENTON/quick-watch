using DTO.CompanyDTOs;

namespace DTO.CommissionDTOs;

public record FullCommissionDTO : BaseCommissionDTO
{
    public Guid Id { get; init; }
    public FullCompanyDTO Company { get; init; }
}