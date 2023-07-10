using DTO.CompanyDTOs;

namespace DTO.ClientDTOs;

public record CreateClientDTO
{
    public CompanyIdDTO Company { get; set; }
}