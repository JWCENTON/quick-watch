using Domain.User.Models;

namespace webapi.Services
{
    public interface IEmailService
    {
        void SendEmailForConfirmation(User user, string confirmationLink);
    }
}
