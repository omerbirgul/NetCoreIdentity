using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityProject.Models;
using IdentityProject.Extensions;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using IdentityProject.Services.RegisterServices;
using System.Security.Policy;
using IdentityProject.Services.EmailServices;

namespace IdentityProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IRegisterService _registerService;
    private readonly IEmailService _emailService;

    public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IRegisterService registerService, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _registerService = registerService;
        _emailService = emailService;
    }



    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public IActionResult SignIn()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignIn(LoginViewModel request, string? returnUrl = null)
    {
        returnUrl = returnUrl ?? Url.Action("Index", "Home");
        var hasUser = await _userManager.FindByEmailAsync(request.Email);
        if (hasUser is null)
        {
            ModelState.AddModelError(string.Empty, "Email or Password Wrong!");
            return View();
        }

        var signInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

        if (signInResult.Succeeded)
        {
            TempData["LoginSuccess"] = "User logged in successfully";
            return Redirect(returnUrl);
        }

        if (signInResult.IsLockedOut)
        {
            ModelState.AddModelErrorList(new List<string> { "Your account has been locked out" });
            return View();
        }

        int accesFailedCount = await _userManager.GetAccessFailedCountAsync(hasUser);
        ModelState.AddModelErrorList(new List<string> { "Email or Password Wrong!", $"Failed loggin attemp: {accesFailedCount}" });
        return View();

    }



    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var result = await _registerService.RegisterAsync(request);
        if (result.Succeeded)
        {
            TempData["Success"] = "User created successfully!";
            return RedirectToAction(nameof(HomeController.Index));
        }

        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
        return View();
    }


    public IActionResult ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
    {
        var hasUser = await _userManager.FindByEmailAsync(model.Email);
        if(hasUser is null)
        {
            ModelState.AddModelError(string.Empty, "Email not found!");
            return View();
        }

        string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
        var passwordResetLink = Url
            .Action("ResetPassword", "Home", new {userId = hasUser.Id, Token = resetPasswordToken}, HttpContext.Request.Scheme, "localhost");

        await _emailService.SendResetEmailAsync(passwordResetLink, hasUser.Email);

        TempData["SuccessMessage"] = "Reset password link has been sent to your email address";
        return RedirectToAction(nameof(HomeController.ForgetPassword));
    }
}