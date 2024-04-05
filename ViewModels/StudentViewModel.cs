using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Teaching.Models;

namespace Teaching.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string? Name { get; set; }
        [Range(0, 120, ErrorMessage = "Age must be between 0 - 120")]
        public int? Age { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? EmailAddress { get; set; }
        [MaxLength(12)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string? PhoneNumber { get; set; }
        public Address? Address { get; set; }
        public SocialMedia? SocialMedia { get; set; }
    }
}
