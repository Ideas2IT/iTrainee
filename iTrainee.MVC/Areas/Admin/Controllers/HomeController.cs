using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using iTrainee.MVC.Helpers;

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

        public IActionResult ManageMentor()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetMentors", "");

            return View(result);
        }

        public IActionResult CreateTrainee()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SaveUser(int id)
        {
            var user = new User();

            if (0 < id)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + id);
            }

            return PartialView(user);
        }

        [HttpPost]
        public int SaveUser(User user)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecutePostApiMethod<User>(baseUrl, "/User/SaveUser", user);

            return 1;
        }
    }
}
