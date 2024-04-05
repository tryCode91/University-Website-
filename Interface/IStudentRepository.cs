using Teaching.Models;

namespace Teaching.Interface
{
    public interface IStudentRepository
    {

        Task<Student> GetStudent(string email);
        Task<Student> GetStudentNoTracking(string email);
        Task<IEnumerable<Course>> GetCourses();
        Task<Course> GetCourse(int courseId);
        public bool Add(Student student);
        public bool AddAbout(UserProfile userProfile);
        public bool AddEducation(Education education);
        public bool AddSkills(Skills skills);
        public bool CreateRecord(StudentCourse studentCourse);
        Task<StudentCourse> AddStudentCourse(int studentId, int courseId);
        public bool AddProfileImage(ProfileImage profileImage);
        public bool UpdateAddressAndSocialMedia(Student student);
        Task<SocialMedia> FormatSocialMediaLinks(SocialMedia socialMedia);
    }
}
