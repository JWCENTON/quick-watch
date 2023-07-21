namespace DTO.UserDTOs;

public record UpdateUserDTO
{
    public string UserName { get; init; }
    public string Email { get; init; }
}