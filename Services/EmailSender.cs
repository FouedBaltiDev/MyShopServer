using System.Net.Mail;

namespace MyShop.Services;

public class EmailSender : IEmailSender
{
    // Injection de dépendance
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;

    public EmailSender(SmtpClient smtpClient, string fromEmail)
    {
        _smtpClient = smtpClient;
        _fromEmail = fromEmail;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_fromEmail),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}
