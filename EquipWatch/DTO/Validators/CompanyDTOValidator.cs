using DTO.CompanyDTOs;
using DTO.EquipmentDTOs;

namespace DTO.Validators;

public class CompanyDTOValidator
{
    public void CreateCompanyDTOValidate(CreateCompanyDTO createCompany)
    {
        if (createCompany.Name.Length < 100)
        {
            
        }
    }
}