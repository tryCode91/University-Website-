using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Teaching.Interface;
using Teaching.Models;
using Teaching.ViewModels;

namespace Teaching.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IStudentRepository studentRepository, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);

            Student student = await _studentRepository.GetStudent(user.Email);

            if (student == null)
                return RedirectToAction("RegisterStudent", "Student");

            if (student.UserProfile == null)
            {
                ViewBag.About = "";
            }

            if (student.Education == null)
            {
                ViewBag.Education = "";
                ViewBag.Career = "";
                ViewBag.Experience = "";
            }
            
            if(student.ProfileImage == null)
            {
                ViewBag.ProfileImage = "";
            }

            if(student.Skills == null)
            {
                ViewBag.Skills = "";
            }

            return View(student);
        }

        [Authorize]
        public async Task<IActionResult> Course()
        {
            IEnumerable<Course> course = await _studentRepository.GetCourses();
            return View(course);
        }

        [Authorize]
        public IActionResult Enroll(int id)
        {
            CourseViewModel courseViewModel = new CourseViewModel();
            return View(courseViewModel);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Enroll(string enrollment, int id)
        {

            if (enrollment == "Yes")
            {
                var user = await _userManager.GetUserAsync(User);

                var student = await _studentRepository.GetStudent(user.Email);

                var add = await _studentRepository.AddStudentCourse(id, student.StudentId);

                if (add.CourseId == 0)
                {
                    // Student already registered on course!
                    ViewBag.StudentEnrolled = "True";
                    return View();
                }

            }

            return RedirectToAction("Course", "Student");

        }

        [Authorize]
        public IActionResult RegisterStudent()
        {
            RegisterStudentViewModel registerStudentViewModel = new RegisterStudentViewModel();
            return View(registerStudentViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel registerStudentViewModel)
        {
            if (!ModelState.IsValid)
                return View("Error", registerStudentViewModel);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return View("Error", registerStudentViewModel);

            var studentVM = new Student
            {
                Name = registerStudentViewModel.Name,
                Age = registerStudentViewModel.Age,
                PhoneNumber = registerStudentViewModel.PhoneNumber,
                EmailAddress = user.Email,
                AppUserId = user.Id,

                Address = new Address()
                {
                    Country = registerStudentViewModel.Address.Country,
                    City = registerStudentViewModel.Address.City,
                    Street = registerStudentViewModel.Address.Street,
                    PostalCode = registerStudentViewModel.Address.PostalCode,
                }

            };

            _studentRepository.Add(studentVM);
            return RedirectToAction("Index", "Student"); // change this
        }

        [Authorize]
        public ActionResult ProfileAbout()
        {
            UserProfile userProfile = new UserProfile();
            return View(userProfile);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileAbout(UserProfileViewModel userProfileViewModel)
        {
            if (!ModelState.IsValid)
                return View("Error", userProfileViewModel);

            var user = await _userManager.GetUserAsync(User);

            var studentId = _studentRepository.GetStudent(user.Email);

            if (studentId == null)
            {
                ModelState.AddModelError(nameof(userProfileViewModel.About), "Could not add user info");
                return View(userProfileViewModel);

            }

            UserProfile userProfile = new UserProfile
            {
                About = userProfileViewModel.About,
                StudentId = studentId.Result.StudentId,
            };

            _studentRepository.AddAbout(userProfile);

            return RedirectToAction("Index", "Student");
        }

        public IActionResult Education()
        {
            EducationViewModel educationViewModel = new EducationViewModel();
            return View(educationViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Education(EducationViewModel educationViewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            var student = await _studentRepository.GetStudent(user.Email);

            var education = new Education()
            {
                EducationDescription = educationViewModel.EducationDescription,
                CareerDescription = educationViewModel.CareerDescription,
                ExperienceDescription = educationViewModel.ExperienceDescription,
                StudentId = student.StudentId,
            };

            _studentRepository.AddEducation(education);

            return RedirectToAction("Index", "Student");
        }

        // Skills
        [Authorize]
        public IActionResult Skills()
        {
            SkillsViewModel skillsViewModel = new SkillsViewModel();
            return View(skillsViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Skills(SkillsViewModel skillsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(skillsViewModel);
            }

            var user = await _userManager.GetUserAsync(User);

            var student = await _studentRepository.GetStudent(user.Email);

            Skills skills = new Skills
            {
                SkillsDescription = skillsViewModel.SkillsDescription,
                ExperienceOne = skillsViewModel.ExperienceOne,
                ExperienceOnePercent = skillsViewModel.ExperienceOnePercent,
                ExperienceTwo = skillsViewModel.ExperienceTwo,
                ExperienceTwoPercent = skillsViewModel.ExperienceTwoPercent,
                ExperienceThree = skillsViewModel.ExperienceThree,
                ExperienceThreePercent = skillsViewModel.ExperienceThreePercent,
                StudentId = student.StudentId
            };

            _studentRepository.AddSkills(skills);
            return RedirectToAction("Index", "Student");

        }

        [Authorize]
        public IActionResult ProfileImage()
        {
            ProfileImage profileImage = new ProfileImage();
            return View(profileImage);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileImage([Bind("ProfileImageId, ImageName, Image")] ProfileImage profileImage)
        {

            if (ModelState.IsValid) {
                
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(profileImage.Image.FileName);
                string extension = Path.GetExtension(profileImage.Image.FileName);
                profileImage.ImageName = fileName = fileName + Guid.NewGuid().ToString() + extension;
                string path = Path.Combine(wwwRootPath + "/image/" + fileName);

                var user = await _userManager.GetUserAsync(User);
                var student = await _studentRepository.GetStudent(user.Email);
                profileImage.StudentId = student.StudentId;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await profileImage.Image.CopyToAsync(fileStream);
                }
            
                _studentRepository.AddProfileImage(profileImage);

                return RedirectToAction("Index", "Student");
            } 
            else
            {
                return View(profileImage);
            }
        }

        [Authorize]
        public async Task<IActionResult> StudentDetail()
        {
            var user = await _userManager.GetUserAsync(User);

            var student = await _studentRepository.GetStudent(user.Email);

            StudentViewModel studentViewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Age = student.Age,
                EmailAddress = student.EmailAddress,
                PhoneNumber = student.PhoneNumber,
                Address = new Address
                {
                    Id = student.Address.Id,
                    Country = student.Address.Country,
                    City = student.Address.City,
                    PostalCode = student.Address.PostalCode,
                    Street = student.Address.Street,
                },
                SocialMedia = new SocialMedia
                {
                    Facebook = student?.SocialMedia?.Facebook,
                    Twitter = student?.SocialMedia?.Twitter,
                    Youtube = student?.SocialMedia?.Youtube,
                    Linkedin = student?.SocialMedia?.Linkedin
                }

            };
            return View(studentViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StudentDetail(StudentViewModel studentViewModel)
        {
            //get current student
            var user = await _userManager.GetUserAsync(User);

            var student = await _studentRepository.GetStudentNoTracking(user.Email);
            
            SocialMedia SMLink = new SocialMedia();

            if(student.SocialMedia != null)
            {
                SMLink = await _studentRepository.FormatSocialMediaLinks(studentViewModel.SocialMedia);
            }

            // Create the the student object and set the new values
            var newStudent = new Student
            {
                StudentId = student.StudentId,
                Name = studentViewModel.Name,
                Age = studentViewModel.Age,
                EmailAddress = studentViewModel.EmailAddress,
                PhoneNumber = studentViewModel.PhoneNumber,
                AppUserId = user.Id,
                UserProfile = student.UserProfile,
                Education = student.Education,
                Skills = student.Skills,
                ProfileImage = student.ProfileImage,
                Address = new Address
                {
                    Country = studentViewModel.Address.Country,
                    City = studentViewModel.Address.City,
                    PostalCode = studentViewModel.Address.PostalCode,
                    Street = studentViewModel.Address.Street,
                },
                SocialMedia = new SocialMedia
                {
                    Facebook = SMLink?.Facebook,
                    Twitter = SMLink?.Twitter,
                    Youtube = SMLink?.Youtube,
                    Linkedin = SMLink?.Linkedin,
                }
            };

            //Update address
            var updateStudent = _studentRepository.UpdateAddressAndSocialMedia(newStudent);

            // if successfull return to Index of Student
            if (updateStudent)
                return RedirectToAction("Index", "Student");
            
            // return to the same view with an error message
            return View("Error");
            
        }
    }

}
