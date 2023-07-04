﻿namespace DTO.EquipmentDTOs;

public record CreateEquipmentDTO
{
    public string SerialNumber { get; init; }
    public string Category { get; init; }
    public string Location { get; init; }
    public int Condition { get; init; }
    public Domain.Company.Models.Company Company { get; init; }
    public bool IsCheckedOut { get; init; }
    public Domain.User.Models.User? CheckedOutBy { get; init; }


}