using System.Net.Mail;

namespace webapi.Services;
public interface ISmtpClientWrapper
{
    Task SendMailAsync(MailMessage message);
}