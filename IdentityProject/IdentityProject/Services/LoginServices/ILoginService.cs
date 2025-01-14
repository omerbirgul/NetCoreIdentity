using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Services.LoginServices
{
    public interface ILoginService
    {
        Task<IdentityResult> SingInAsync(LoginViewModel request, string? returnUrl = null);
    }
}
