using Microsoft.AspNetCore.Mvc;

namespace Teaching.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
