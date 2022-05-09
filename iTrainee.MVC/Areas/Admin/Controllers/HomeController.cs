using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using iTrainee.MVC.Helpers;
using System.Data.SqlClient;

namespace iTrainee.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;
        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageMentors()
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

        public IActionResult ManageMentor()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetMentors");

            return View(result);
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

        [HttpGet]
        public IActionResult SaveUser(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + id);

            return PartialView(user);
        }

        [HttpPost]
        public int SaveUser(User user)
        {
            if (user.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<User>(baseUrl, "/User/SaveUser", user);
            }
            else
            {

            }

            return 1;
        }

     
        public IActionResult DeleteUser(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Stream>(baseUrl, "/User/DeleteUser?", "Id=" + id);
            return new JsonResult("");
        }
    }
}
