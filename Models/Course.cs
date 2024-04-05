using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teaching.Data;

namespace Teaching.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
