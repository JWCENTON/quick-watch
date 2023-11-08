using DAL;
using System.Net.Mail;
using System.Net;

namespace webapi.Services;

public class SmtpClientWrapper : ISmtpClientWrapper
{
    private readonly SmtpClient _smtpClient;

    public SmtpClientWrapper(string smtpServer, int port, string userName, string password)
    {
        _smtpClient = new SmtpClient(smtpServer, port)
        {
            Credentials = new NetworkCredential(userName, password),
            EnableSsl = true
        };
    }

    public async Task SendMailAsync(MailMessage message)
    {
        await _smtpClient.SendMailAsync(message);
    }
}