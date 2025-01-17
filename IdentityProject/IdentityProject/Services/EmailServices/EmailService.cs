
using IdentityProject.OptionModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityProject.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetEmailAsync(string resetLink, string receiver)
        {
            var smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Host = _emailSettings.Host;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(receiver);
            mailMessage.Subject = "Reset Password Link";
            mailMessage.Body = 
                $"<h4> Please click the button below to reset your password</h4>" +
                $"<a href='{resetLink}'> Reset Password Link </a>";

            mailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
