using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Teaching.Interface;
using Teaching.Models;
using Teaching.ViewModels;

namespace Teaching.Controllers
{
    public class MailController : Controller
    {
        private readonly IMailRepository _mailRepository;
        private readonly UserManager<AppUser> _userManager;

        public MailController(IMailRepository mailRepository, UserManager<AppUser> userManager)
        {
            _mailRepository = mailRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MailViewModel mailViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAuthorized = false;
            //if user is not logged we do not need the mailmodel FROM so we do not need to check if every property in mailmodel is inserted
            if (user != null)
            {
                mailViewModel.From = user.Email;
                isAuthorized = true;
            } 
            else
            {
                if (!ModelState.IsValid)
                    return View("Index", mailViewModel);
            }

            try
            {
                //Send email
                await _mailRepository.SendEmailAsync(mailViewModel, isAuthorized);
                ViewBag.IsSent = true;
                return View();

            }
            catch (Exception ex)
            {
                throw;
            }   
        }

    }
}
