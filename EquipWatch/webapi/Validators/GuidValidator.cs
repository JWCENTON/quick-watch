namespace DTO.Validators;

public static class GuidValidator
{
    public static bool ValidateGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}