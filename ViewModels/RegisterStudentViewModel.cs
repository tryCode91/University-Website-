using System.ComponentModel.DataAnnotations;
using Teaching.Models;

namespace Teaching.ViewModels
{
    public class RegisterStudentViewModel
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
    }
}
