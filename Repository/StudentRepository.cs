using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Teaching.Data;
using Teaching.Interface;
using Teaching.Models;

namespace Teaching.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StudentRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Student> GetStudent(string email)
        {
            return await _context.Student.Include(sm => sm.SocialMedia).Include(pi => pi.ProfileImage).Include(s => s.Skills).Include(e => e.Education).Include(u => u.UserProfile).Include(p => p.Address).Include(c => c.Course).Where(u => u.EmailAddress == email).FirstOrDefaultAsync();
        }

        public async Task<Student> GetStudentNoTracking(string email)
        {
            return await _context.Student.Include(sm => sm.SocialMedia).Include(pi => pi.ProfileImage).Include(s => s.Skills).Include(e => e.Education).Include(u => u.UserProfile).Include(p => p.Address).Include(c => c.Course).Where(u => u.EmailAddress == email).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context.Course.ToListAsync();
        }

        public bool Add(Student student)
        {
            _context.Add(student);
            return Save();   
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool AddAbout(UserProfile userProfile)
        {
            _context.Add(userProfile);
            return Save();
        }

        public bool AddEducation(Education education)
        {
            _context.Add(education);
            return Save();
        }

        public bool AddSkills(Skills skills)
        {
            _context.Add(skills);
            return Save();
        }

        public bool AddStudentToCourse(StudentCourse studentCourse)
        {
            _context.Add(studentCourse);
            return Save();
        }

        public bool CreateRecord(StudentCourse studentCourse)
        {
            _context.Add(studentCourse);
            return Save();
        }

        public async Task<StudentCourse> AddStudentCourse(int courseId, int studentId)
        {

            var checkRecord = _context.StudentCourse.Where(u => u.StudentId == studentId && u.CourseId == courseId).Any();
            
            // Student and course are not joined together in join table
            if (!checkRecord)
            {
                // Add record
                StudentCourse studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                CreateRecord(studentCourse);
                return studentCourse;
                
            }
            else
            {
                StudentCourse studentCourse = new StudentCourse();

                return studentCourse;
            }

        }

        public async Task<Course> GetCourse(int courseId)
        {
            return await _context.Course.FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        public bool AddProfileImage(ProfileImage profileImage)
        {
            _context.Add(profileImage);
            return Save();
        }

        public bool UpdateAddressAndSocialMedia(Student student)
        {
            _context.Update(student);
            return Save();
        }

        public async Task<SocialMedia> FormatSocialMediaLinks(SocialMedia socialMedia)
        {
            string https = "https://";

            if(socialMedia.Facebook != null)
            {
                if (!socialMedia.Facebook.Contains(https))
                {
                    socialMedia.Facebook = Regex.Replace(https + socialMedia.Linkedin.ToString(), @"\s+", "");
                }
                
                if (!socialMedia.Twitter.Contains(https))
                {
                    socialMedia.Twitter = Regex.Replace(https + socialMedia.Linkedin.ToString(), @"\s+", "");
                }
                
                if (!socialMedia.Youtube.Contains(https))
                {
                    socialMedia.Youtube = Regex.Replace(https + socialMedia.Linkedin.ToString(), @"\s+", "");
                }
                
                if (!socialMedia.Linkedin.Contains(https))
                {
                    socialMedia.Linkedin = Regex.Replace(https + socialMedia.Linkedin.ToString(), @"\s+", "");
                }
                
            }

            return socialMedia;
        }
    }
}
