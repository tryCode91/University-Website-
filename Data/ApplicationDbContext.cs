using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using Teaching.Models;

namespace Teaching.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
  
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
    }
}
