namespace DTO.UserDTOs;

public record LoginUserDTO
{
    public string Email { get; init; }
    public string Password { get; init; }
}