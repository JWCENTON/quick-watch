namespace Domain.Invite.Models;

public class Invite
{
    public Guid Id { get; set; }
    public Company.Models.Company Company { get; set; }
    public User.Models.User User { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}