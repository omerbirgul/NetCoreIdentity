using IdentityProject.CustomValidations;
using IdentityProject.Models;
using IdentityProject.Services.LoginServices;
using IdentityProject.Services.RegisterServices;
using IdentityProject.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Extensions
{
    public static class ApplicationExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromMinutes(3);
            });



            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder();
                cookieBuilder.Name = "AppCookie";
                opt.LoginPath = new PathString("/Home/Signin");
                opt.LogoutPath = new PathString("/Member/SignOut");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                opt.SlidingExpiration = true;
            });


            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            })
                .AddPasswordValidator<PasswordValidator>() // Password validasyon için
                .AddUserValidator<UserValidator>()         // User validasyon için
                .AddDefaultTokenProviders()                // Token ekleyip kullanmak için
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
