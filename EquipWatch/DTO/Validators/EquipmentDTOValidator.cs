using DTO.EquipmentDTOs;

namespace DTO.Validators;

public class EquipmentDTOValidator
{
    public void CreateEquipmentDTOValidate(CreateEquipmentDTO createEquipment)
    {
        if (createEquipment.SerialNumber.Length != 10)
        {
            //throw new ArgumentException("Invalid serial number");
        }
    }
}