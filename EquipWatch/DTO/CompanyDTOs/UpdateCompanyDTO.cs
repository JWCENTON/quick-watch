namespace DTO.CompanyDTOs;

public record UpdateCompanyDTO
{
    public string? Name { get; init; }
    public string? OwnerId { get; init; }
}