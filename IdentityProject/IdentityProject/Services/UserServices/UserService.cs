using IdentityProject.Areas.Admin.Models;
using IdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserViewModel>> GetUserListAsync()
        {
            var userViewModelList = await _userManager.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Name = x.UserName,
                Email = x.Email
            }).ToListAsync();
            return userViewModelList;
        }
    }
}
