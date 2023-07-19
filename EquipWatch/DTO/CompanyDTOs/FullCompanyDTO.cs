namespace DTO.CompanyDTOs;

public record FullCompanyDTO : BaseCompanyDTO
{
    public Guid Id { get; init; }
}