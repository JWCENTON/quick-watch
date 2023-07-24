namespace DTO.UserDTOs;

public record BaseUserDTO
{
    public string Email { get; init; }
    public string Password { get; init; }
};