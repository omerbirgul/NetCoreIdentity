using System.ComponentModel.DataAnnotations;

namespace IdentityProject.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        public string NewPasswordConfirm { get; set; }
    }
}
