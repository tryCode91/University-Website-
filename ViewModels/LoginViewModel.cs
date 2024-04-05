using System.ComponentModel.DataAnnotations;

namespace Teaching.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Enter you email address"), Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Enter your password"), Required]
        public string Password { get; set; }
    }
}
