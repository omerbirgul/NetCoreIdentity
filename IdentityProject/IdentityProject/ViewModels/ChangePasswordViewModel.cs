using System.ComponentModel.DataAnnotations;

namespace IdentityProject.ViewModels;

    //public class ChangePasswordViewModel
    //{
    //}

public record ChangePasswordViewModel
{
    [Required]
    public string OldPassword { get; init; }
    [Required]
    public string NewPassword { get; init; }
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; init; }
}

