using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Services.RegisterServices
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel request);
    }
}
