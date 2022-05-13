﻿using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace iTrainee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
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

        public IActionResult Login()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUserByUserName?", "UserName=" + UserName + "&Password=" + Password);
            TempData["HeaderRole"] = user.RoleName;
            return RedirectToAction("Index", "Home", new {Area = user.RoleName});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            string UserName = "selvaraj@29";
            string Password = "Selvaraj2023";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUserByUserName?", "UserName=" + UserName + "&Password=" + Password);
            TempData["HeaderRole"] = "Admin";
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(User user)
        {
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
