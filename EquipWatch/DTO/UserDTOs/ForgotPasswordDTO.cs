using System.ComponentModel.DataAnnotations;

namespace DTO.UserDTOs
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
