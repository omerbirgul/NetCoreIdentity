using IdentityProject.Extensions;
using IdentityProject.Models;
using IdentityProject.Services.LoginServices;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public IActionResult SignIn()
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

            var signInResult =  await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);
            
            if (signInResult.Succeeded)
            {
                TempData["LoginSuccess"] = "User logged in successfully";
                return Redirect(returnUrl ?? "/");
            }

            if(signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string> { "Your account has been locked out" });
                return View();
            }
            
            int accesFailedCount = await _userManager.GetAccessFailedCountAsync(hasUser);
            ModelState.AddModelErrorList(new List<string> { "Email or Password Wrong!", $"Failed loggin attemp: {accesFailedCount}" });
            return View();
            //return RedirectToAction("Index", "Home");
        }
    }
}
