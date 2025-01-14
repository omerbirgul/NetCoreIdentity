using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult SingIn()
        {
            return View();
        }
    }
}
