namespace DTO.UserDTOs;

public record UpdateUserDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
}