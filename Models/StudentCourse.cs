using System.ComponentModel.DataAnnotations;

namespace Teaching.Models
{
    public class StudentCourse
    {
        [Key]
        public int StudentCourseId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
