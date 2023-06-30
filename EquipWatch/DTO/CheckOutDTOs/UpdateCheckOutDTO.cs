namespace DTO.CheckOutDTOs;

public class UpdateCheckOutDTO
{
    public Domain.User.Models.User User { get; set; }
    public DateTime Time { get; set; }
}