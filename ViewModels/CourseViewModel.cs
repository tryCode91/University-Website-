using System.ComponentModel.DataAnnotations;

namespace Teaching.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
