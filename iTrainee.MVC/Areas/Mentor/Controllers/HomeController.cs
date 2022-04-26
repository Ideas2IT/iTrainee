using Microsoft.AspNetCore.Mvc;

namespace iTrainee.MVC.Areas.Mentor.Controllers
{
    [Area("Mentor")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
