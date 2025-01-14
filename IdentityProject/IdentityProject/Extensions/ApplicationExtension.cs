using IdentityProject.CustomValidations;
using IdentityProject.Models;
using IdentityProject.Services.RegisterServices;
using IdentityProject.Services.UserServices;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Extensions
{
    public static class ApplicationExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IUserService, UserService>();

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
            })
                .AddPasswordValidator<PasswordValidator>()
                .AddUserValidator<UserValidator>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
