using AutoMapper;
using DAL.Repositories.Company;
using Domain.Company.Models;
using DTO.EquipmentDTOs;

namespace DTO.Validators;

public class EquipmentDTOValidator
{
    public EquipmentDTOValidator()
    {
    }

    public void CreateEquipmentDTOValidate(CreateEquipmentDTO createEquipment)
    {
        if (createEquipment.SerialNumber.Length != 10)
        {
            
        }
    }
}