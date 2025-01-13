using IdentityProject.Models;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Services.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.Phone
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            return result;
        }
    }
}
