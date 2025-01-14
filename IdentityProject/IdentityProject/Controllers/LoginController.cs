using IdentityProject.Models;
using IdentityProject.Services.LoginServices;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //private readonly ILoginService _loginService;

        //public LoginController(ILoginService loginService)
        //{
        //    _loginService = loginService;
        //}

        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel request, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? string.Empty;
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser is null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password Wrong!");
                return View();
            }

            var signInResult =  await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, false);
            if (signInResult.Succeeded)
            {
                TempData["LoginSuccess"] = "User logged in successfully";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email or Password Wrong!");
            return View();
        }
    }
}
