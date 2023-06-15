using Microsoft.AspNetCore.Identity;

namespace Domain.User.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}