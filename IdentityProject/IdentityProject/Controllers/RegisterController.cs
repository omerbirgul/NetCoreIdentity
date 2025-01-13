using IdentityProject.Services.RegisterServices;
using IdentityProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        // GET: RegisterController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(RegisterViewModel request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = await _registerService.RegisterAsync(request);
            if (result.Succeeded)
            {
                TempData["Success"] = "User created successfully!";
                return RedirectToAction(nameof(RegisterController.Index));
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

    }
}
