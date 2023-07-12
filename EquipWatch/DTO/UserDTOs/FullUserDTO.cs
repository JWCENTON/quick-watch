using Microsoft.AspNetCore.Identity;

namespace DTO.UserDTOs;

public record FullUserDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}