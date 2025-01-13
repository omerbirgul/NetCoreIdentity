using IdentityProject.Areas.Admin.Models;

namespace IdentityProject.Services.UserServices
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUserListAsync();
    }
}
