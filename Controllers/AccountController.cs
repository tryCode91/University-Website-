using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Teaching.Data;
using Teaching.Models;
using Teaching.ViewModels;

namespace Teaching.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            var registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var isChecked = registerViewModel.AgreeTOS;

            if(!isChecked)
                return View(registerViewModel);

            if (ModelState.IsValid)
            {
                // Check if user already exist
                var checkUser = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

                if (checkUser == null)
                {
                    // if not exist, create new user object
                    var user = new AppUser
                    {
                        Email = registerViewModel.EmailAddress,
                        UserName = registerViewModel.EmailAddress
                    };

                    //Create the user
                    var newUserResponse = await _userManager.CreateAsync(user, registerViewModel.Password);

                    //Add the role of the user, will always be a student
                    if (newUserResponse.Succeeded)
                        await _userManager.AddToRoleAsync(user, UserRoles.Student);
                    // Crumb show error message on input validation.
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError(nameof(registerViewModel.EmailAddress), "Email already in use");
                    return View(registerViewModel);
                }

            }

            return View(registerViewModel);

        }
        
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

                if(user != null)
                {
                    var password = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                    if (password)
                    {
                        var authorize = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                        if (authorize.Succeeded)
                        {
                            return RedirectToAction("Index", "Student");
                        }
                    }

                    ModelState.AddModelError(nameof(loginViewModel.EmailAddress), "Wrong credentials, please try again");
                    return View(loginViewModel);
                }

                ModelState.AddModelError(nameof(loginViewModel.EmailAddress), "Could not authorize user");
                return View(loginViewModel);
                
            }

            ModelState.AddModelError(nameof(loginViewModel.EmailAddress), "Please enter your credentials");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
