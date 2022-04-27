using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System.Collections.Generic;
using System;

namespace iTrainee.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageMentor()
        {
            List<User> mentors = new List<User>();
            User user = new User();
            user.Id = 1;
            user.FirstName = "user1";
            user.LastName = "lastname1";
            user.Qualification = "B.E";
            user.UserName = "my userid";
            DateTime date2 = new DateTime(2012, 12, 25, 10, 30, 50);

            user.DOB = date2;

            mentors.Add(user);

            return View(mentors);
        }

        public IActionResult CreateTrainee()
        {
            return View();
        }

        public IActionResult ManageTrainee()
        {
            List<User> trainees = new List<User>();
            User user = new User();
            user.Id = 1;
            user.FirstName = "Trainee1";
            user.LastName = "Trainee 1";
            user.Qualification = "B.E";
            user.UserName = "my userid";
            DateTime date2 = new DateTime(2012, 12, 25, 10, 30, 50);

            user.DOB = date2;

            trainees.Add(user);

            return View(trainees);
        }
    }
}
