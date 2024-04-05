using System.ComponentModel.DataAnnotations;

namespace Teaching.ViewModels
{
    public class MailViewModel
    {
        [Required]
        [EmailAddress]
        public string From { get; set; }
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
