namespace MyShop.Services;

public interface IEmailSender
{
    // Keyword Task en c# définit une méthode asynchrone à voir dans la formation
    Task SendEmailAsync(string email, string subject, string message);
}

