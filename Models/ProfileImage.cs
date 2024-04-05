using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teaching.Models
{
    public class ProfileImage
    {
        [Key]
        public int ProfileImageId { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile Image { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
