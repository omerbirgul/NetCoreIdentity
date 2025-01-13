using IdentityProject.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var userViewModelList = await _userService.GetUserListAsync();
            return View(userViewModelList);
        }
    }
}
