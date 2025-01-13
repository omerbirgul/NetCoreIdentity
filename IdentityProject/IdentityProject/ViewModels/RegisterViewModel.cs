using System.ComponentModel.DataAnnotations;

namespace IdentityProject.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
