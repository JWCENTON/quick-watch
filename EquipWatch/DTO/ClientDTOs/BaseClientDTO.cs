namespace DTO.ClientDTOs;

public record BaseClientDTO
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string ContactAddress { get; init; }
};