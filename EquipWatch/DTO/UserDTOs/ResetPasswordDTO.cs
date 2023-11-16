using System.ComponentModel.DataAnnotations;

namespace DTO.UserDTOs;

public class ResetPasswordDTO
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    public string PasswordResetToken { get; set; }
    public DateTime PasswordResetExpiration { get; set; }
}