namespace DTO.UserDTOs;

public record BaseUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; init; }
    public string Password { get; init; }
};