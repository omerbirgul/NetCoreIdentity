using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            if(password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = "PasswordContainsUserName", Description = "Password cannot contain username" }));
            }

            if (password.Contains("1234"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "PasswordContainsSequence",
                    Description = "Password cannot contain numeric sequence"
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
