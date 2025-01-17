namespace IdentityProject.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendResetEmailAsync(string resetLink, string receiver);
    }
}
