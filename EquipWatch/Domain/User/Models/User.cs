using Microsoft.AspNetCore.Identity;

namespace Domain.User.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordResetToken { get; set; }
    public DateTime PasswordResetExpiration { get; set; }
}