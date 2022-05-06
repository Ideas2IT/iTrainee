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
    [Route("Admin/[controller]/[action]/{id?}")]
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

        public IActionResult ManageUser(string role)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?", "role=" + role);

            ViewBag.Role = role;
            TempData.Remove("Role");
            TempData.Add("Role", role);
            return View(result);
        }

        public IActionResult SaveUser(int id)
        {
            var user = new User();
            if (Convert.ToString(TempData["Role"]) == "Admin")
            {
                user.IsAdmin = true;
            }
            else if (Convert.ToString(TempData["Role"]) == "Mentor")
            {
                user.IsMentor = true;
            }
            else if (Convert.ToString(TempData["Role"]) == "Trainee")
            {
                user.IsTrainee = true;
            }

            if (0 < id)
            {
                TempData.Remove("UserId");
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + id);
                TempData.Add("UserId", id);
            }

            return PartialView(user);
        }

        [HttpPost]
        public int SaveUser(User user)
        {
            if (0 < user.Id)
            {
                user.Id = Convert.ToInt32(TempData["UserId"]);
            }

            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecutePostApiMethod<User>(baseUrl, "/User/SaveUser", user);

            return 1;
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Stream>(baseUrl, "/User/DeleteUser?", "Id=" + id);
            return new JsonResult("");
        }
    }
}
