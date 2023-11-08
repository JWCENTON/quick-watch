using Domain.User.Models;

namespace webapi.Services;

public interface IEmailService
{
    Task SendEmailForConfirmationAsync(User user, string confirmationLink);
    Task SendPasswordResetLinkAsync(User user, string resetLink);
}