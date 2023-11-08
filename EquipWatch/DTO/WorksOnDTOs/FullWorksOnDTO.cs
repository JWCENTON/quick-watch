namespace DTO.WorksOnDTOs;

public record FullWorksOnDTO() : BaseWorksOnDTO
{
    public string Id { get; init; }
};