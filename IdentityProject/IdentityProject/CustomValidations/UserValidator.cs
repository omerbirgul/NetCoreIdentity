using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityProject.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var isNumeric = int.TryParse(user.UserName[0]!.ToString(), out _);

            if(isNumeric)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNameFirstLetterContainNumeric",
                    Description = "Username's first letter cannot be numeric characters"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
