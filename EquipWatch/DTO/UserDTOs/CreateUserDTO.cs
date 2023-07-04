namespace DTO.UserDTOs;

public record CreateUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}