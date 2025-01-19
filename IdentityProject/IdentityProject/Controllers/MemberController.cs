using IdentityProject.Models;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userViewModel = new UserViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return View(userViewModel);
        }


        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View();
            }

            var hasUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if(hasUser is null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }

            var checkOldPassword = await _userManager.CheckPasswordAsync(hasUser, model.OldPassword);
            if (!checkOldPassword)
            {
                ModelState.AddModelError("", "Old password is wrong");
                return View();
            }

            var identityResult = await _userManager.ChangePasswordAsync(hasUser,model.OldPassword,model.NewPassword);
            if (!identityResult.Succeeded)
            {
                ModelState.AddModelError("", "Password could not be changed");
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(hasUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(hasUser, model.NewPassword, true, false);

            ViewData["Success"] = "Password changed successfully";
            return View();
        }
    }
}
