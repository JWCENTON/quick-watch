namespace Domain.Booking.DTO;

public class UpdateBookingDTO
{
    public Client.Models.Client Client { get; set; }
    public string Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}