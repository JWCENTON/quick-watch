using System.ComponentModel.DataAnnotations;

namespace DTO.UserDTOs;

public class ChangePasswordDTO
{
    [Required]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}