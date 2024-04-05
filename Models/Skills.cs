using System.ComponentModel.DataAnnotations;

namespace Teaching.Models
{
    public class Skills
    {
        [Key]
        public int SkillsId { get; set; }
        public string? SkillsDescription { get; set; }
        public string ExperienceOne { get; set; }
        public int ExperienceOnePercent { get; set; }
        public string ExperienceTwo { get; set; }
        public int ExperienceTwoPercent { get; set; }
        public string ExperienceThree { get; set; }
        public int ExperienceThreePercent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
