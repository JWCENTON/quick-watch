using Domain.Commission.Models.Commission;
using Domain.Company.Models;
using Domain.User.Models;

namespace DTO.EquipmentDTOs;

public record PartialEquipmentDTO
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    public bool IsCheckedOut { get; set; }
}