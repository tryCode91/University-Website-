using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teaching.Models
{
    public class SocialMedia
    {
        [Key]
        public int SocialMediaId { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Youtube { get; set; }
        public string? Linkedin { get; set; } 
    }
}
