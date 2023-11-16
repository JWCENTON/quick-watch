using DAL;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Domain.User.Models;

namespace webapi.Services;

public class EmailService : IEmailService
{
    private readonly EmailContext _emailContext;

    public EmailService(IOptions<EmailContext> emailContext)
    {
        _emailContext = emailContext.Value;
    }

    public async Task SendEmailForConfirmationAsync(User user, string confirmationLink)
    {
        using (var client = new SmtpClient(_emailContext.Smtp, _emailContext.Port))
        {
            client.Credentials = new NetworkCredential(_emailContext.UserName, _emailContext.Password);
            client.EnableSsl = true;

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_emailContext.Address!);
                message.To.Add(new MailAddress(user.Email!));

                message.Subject = "Equip Watch - Registration";
                message.Body = $"Dear {user.FirstName} {user.LastName}," +
                               $"\n\nThank you for registration in Equip Watch" +
                               $"\n\nPlease confirm your email address by clicking on the below link" +
                               $"\n\n{confirmationLink}\n\nBest Regards,\nEquip Watch";

                await client.SendMailAsync(message);
            }
        }
    }

    public async Task SendPasswordResetLinkAsync(User user, string resetLink)
    {
        using (var client = new SmtpClient(_emailContext.Smtp, _emailContext.Port))
        {
            client.Credentials = new NetworkCredential(_emailContext.UserName, _emailContext.Password);
            client.EnableSsl = true;

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_emailContext.Address!);
                message.To.Add(new MailAddress(user.Email!));

                message.Subject = "Equip Watch - Password Reset";
                message.Body = $"Dear {user.FirstName} {user.LastName}," +
                               $"\n\nYou have requested to reset your password for Equip Watch." +
                               $"\n\nPlease click on the below link to reset your password:" +
                               $"\n\n{resetLink}\n\nIf you did not request this, please ignore this email.\n\nBest Regards,\nEquip Watch";

                await client.SendMailAsync(message);
            }
        }
    }
}