namespace DTO.UserDTOs;

public record PartialUserDTO()
{
    public string Id { get; init; }
    public string UserName { get; init; }
};