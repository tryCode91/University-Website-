using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teaching.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? EmailAddress{ get; set; }
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId{ get; set; }
        public AppUser? AppUser { get; set; }
        public UserProfile? UserProfile { get; set; }
        public Education? Education { get; set; }
        public Skills? Skills { get; set; }
        public ProfileImage? ProfileImage { get; set; }
        [ForeignKey("SocialMedia")]
        public int? SocialMediaId { get; set; }
        public SocialMedia? SocialMedia { get; set; }
    }
}
