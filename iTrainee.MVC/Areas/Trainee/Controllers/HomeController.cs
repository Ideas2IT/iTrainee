using Microsoft.AspNetCore.Mvc;

namespace iTrainee.MVC.Areas.Trainee.Controllers
{
    [Area("Trainee")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
