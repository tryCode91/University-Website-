using System.ComponentModel.DataAnnotations;

namespace Teaching.ViewModels
{
    public class SkillsViewModel
    {
        public int SkillsId { get; set; }

        [StringLength(300, ErrorMessage = "Max 300 characters")]
        public string? SkillsDescription { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string? ExperienceOne { get; set; }
        
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int ExperienceOnePercent { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string? ExperienceTwo { get; set; }
        
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int ExperienceTwoPercent { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string? ExperienceThree { get; set; }
        
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int ExperienceThreePercent { get; set; }
    }
}
