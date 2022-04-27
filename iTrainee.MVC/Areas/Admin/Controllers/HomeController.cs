using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowMentor()
        {
            List<User> mentor = new List<User>();

            return View(mentor);
        }

        public IActionResult CreateMentor()
        {
            return View();
        }

        public IActionResult CreateTrainee()
        {
            return View();
        }

        public IActionResult ShowTrainee()
        {
            List<User> trainees = new List<User>();

            return View(trainees);
        }
    }
}
