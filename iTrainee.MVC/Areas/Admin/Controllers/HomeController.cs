using Microsoft.AspNetCore.Mvc;

namespace iTrainee.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowMentors()
        {
            return View();
        }

        public IActionResult CreateTrainee()
        {
            return View();
        }
    }
}
