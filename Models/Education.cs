using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teaching.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string? EducationDescription { get; set; }
        public string? CareerDescription { get; set; }
        public string? ExperienceDescription { get; set; }
        public int? StudentId { get; set; }
        public Student? Student{ get; set; }
    }
}
