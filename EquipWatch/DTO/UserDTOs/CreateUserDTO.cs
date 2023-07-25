namespace DTO.UserDTOs;

public record CreateUserDTO : BaseUserDTO
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}