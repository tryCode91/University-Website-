using System.ComponentModel.DataAnnotations;

namespace Teaching.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "TOS")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You have to agree to the TOS")]
        public bool AgreeTOS { get; set; }
    }
}
