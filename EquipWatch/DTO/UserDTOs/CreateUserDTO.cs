using Domain.User.Models;
using Microsoft.AspNetCore.Identity;

namespace DTO.UserDTOs;

public record CreateUserDTO
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}