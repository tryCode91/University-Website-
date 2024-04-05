using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teaching.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public string? About { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
