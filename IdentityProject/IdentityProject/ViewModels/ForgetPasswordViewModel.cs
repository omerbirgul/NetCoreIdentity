using System.ComponentModel.DataAnnotations;

namespace IdentityProject.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Input type shoul be Email format")]
        public string Email { get; set; }
    }
}
