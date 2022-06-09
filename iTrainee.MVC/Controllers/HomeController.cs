using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace iTrainee.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
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

        public IActionResult Login(User user)
        {
            if (Convert.ToString(TempData["IsValidUserName"]) == "false")
            {
                TempData["IsValidUserName"] = "false";
            } else
            {
                TempData["IsValidUserName"] = "true";
            }

            if (Convert.ToString(TempData["IsValidPassword"]) == "false")
            {
                TempData["IsValidPassword"] = "false";
            } else
            {
                TempData["IsValidPassword"] = "true";
            }

            return View(user);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUserByUserName?", "UserName=" + UserName + "&Password=" + Password,"");
           
            if (user.UserName == null)
            {
                TempData["IsValidUserName"] = "false";
                user.UserName = UserName;
                return RedirectToAction("Login", user);
            }
            else if(user.Password == null)
            {
                TempData["IsValidPassword"] = "false";
                return RedirectToAction("Login", user);
            }

            HttpContext.Session.SetString("username", user.UserName);

            HttpContext.Session.SetString("username", user.UserName);

            TempData["UserId"] = user.Id;
            TempData["HeaderRole"] = user.RoleName;
            TempData["CurrentUserName"] = user.FirstName + " " + user.LastName;
            TempData["UserFirstName"] = user.FirstName;           
            TempData["UserToken"] = user.Token;
            TempData["UnreadMessagesCount"] = user.UnreadMessagesCount;
            var token = Convert.ToString(TempData["UserToken"]);

            if (user.RoleName.Equals("Trainee"))
            {
                UserAudit userAudit = new UserAudit();
                userAudit.UserId = user.Id;
                userAudit.Date = DateTime.Now;
                userAudit.SignIn = DateTime.Now;
                userAudit.SignOut = DateTime.Now;
                int userAuditId = HttpClientHelper.ExecuteInsertPostApiMethod<UserAudit>(baseUrl, "/UserAudit/InsertUserAudit", userAudit, token);

                return RedirectToAction("Index", "Home", new { Area = "Trainee", auditId = userAuditId, userId = user.Id, traineeName = user.FirstName });
            }
            else if (user.RoleName.Equals("Mentor"))
            {
                return RedirectToAction("Index", "Home", new { Area = "Mentor", userId = user.Id });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { Area = "Admin", userId = 0 });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult ChangePassword(int userId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + userId, Convert.ToString(TempData["UserToken"]));
            TempData["CurrentPassword"] = user.Password;
            TempData["UserId"] = user.Id;
            TempData["HeaderRole"] = "Admin";
            
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult ChangePassword(string updatedPassword , int userId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + userId, Convert.ToString(TempData["UserToken"]));
            user.Password = updatedPassword;
            HttpClientHelper.ExecutePostApiMethod<User>(baseUrl, "/User/UpdateUser" , user, Convert.ToString(TempData.Peek("UserToken")));
            return RedirectToAction("Login", new { user = new User() });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Logout()
        { 
            if (HttpContext.Session != null)
            {
                 HttpContext.Session.Clear();
                // HttpContext.Session.Remove("username");
            }

            if (HttpContext.Session == null)
            {
                HttpContext.Session.Remove("username");
            }

            return RedirectToAction("Login");
        }

    }
}


