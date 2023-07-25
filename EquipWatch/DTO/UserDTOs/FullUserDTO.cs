namespace DTO.UserDTOs;

public record FullUserDTO : BaseUserDTO
{
    public string Id { get; init; }
    public string UserName { get; init; }
}