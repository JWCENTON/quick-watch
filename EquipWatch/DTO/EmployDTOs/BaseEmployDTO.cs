﻿using Domain.Employee;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EmployDTOs;

public record BaseEmployDTO
{
    public string CompanyId { get; init; }
    public string UserId { get; init; }
    public Role Role { get; init; }
};