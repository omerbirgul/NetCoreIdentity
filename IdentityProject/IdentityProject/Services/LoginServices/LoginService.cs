using IdentityProject.Models;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace IdentityProject.Services.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<IdentityResult> SingInAsync(LoginViewModel request, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? string.Empty;
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if(hasUser is null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailOrPasswordWrong!",
                    Description = "Email or Password Wrong!"
                });
            }

            var signInResult = _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, false);
            if (!signInResult.IsCompletedSuccessfully)
            {
                return IdentityResult.Failed(new IdentityError { });
            }

            return IdentityResult.Success;
        }
    }
}
