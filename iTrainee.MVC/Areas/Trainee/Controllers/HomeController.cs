using Microsoft.AspNetCore.Mvc;

namespace iTrainee.MVC.Areas.Trainee.Controllers
{
    [Area("Trainee")]
    [Route("Trainee/[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["HeaderRole"] = "Trainee";
            TempData.Peek("UserToken");
            return View();
        }
    }
}
