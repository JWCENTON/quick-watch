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
        //if (createEquipment.IsCheckedOut)
        //{
        //    //get user from userRepository and check if its exist
        //}
        // other if statements like the one bellow
        if (createEquipment.SerialNumber.Length != 10)
        {
            
        }


    }
}