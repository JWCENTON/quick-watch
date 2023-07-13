using DAL;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Domain.User.Models;

namespace webapi.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailContext _emailContext;

        public EmailService(IOptions<EmailContext> emailContext)
        {
            _emailContext = emailContext.Value;
        }

        public void SendEmailForConfirmation(User user, string confirmationLink)
        {
            // Create a new SmtpClient instance with your SMTP server details
            var client = new SmtpClient(_emailContext.Smtp, _emailContext.Port)
            {
                Credentials = new NetworkCredential(_emailContext.Username, _emailContext.Password),
                EnableSsl = true
            };

            // Create a new MailMessage instance
            var message = new MailMessage();

            // Set the sender and recipient email addresses
            message.From = new MailAddress(_emailContext.Address);
            message.To.Add(new MailAddress(user.Email));

            // Set the subject and body of the email
            message.Subject = "Equip Watch - Registration";
            message.Body = $"Dear {user.FirstName} {user.LastName}," +
                           $"\n\nThank you for registration in Equip Watch" +
                           $"\n\nPlease confirm your email address by clicking on the below link" +
                           $"\n\n{confirmationLink}\n\nBest Regards,\nEquip Watch";

            // Send the email using the SmtpClient
            client.Send(message);
        }
    }
}
