using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace iTrainee.Controllers
{
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
            UserAudit userAudit = new UserAudit();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUserByUserName?", "UserName=" + UserName + "&Password=" + Password);
           
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

            TempData["HeaderRole"] = user.RoleName;
            TempData["CurrentUserName"] = user.FirstName + " " + user.LastName;
            TempData["UserId"] = user.Id;
            TempData["UserToken"] = user.Token;
            var token = Convert.ToString(TempData["UserToken"]);
            userAudit.UserId = user.Id;
            userAudit.Date = DateTime.Now.Date;
            userAudit.SignIn = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            userAudit.SignOut = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            int userAuditId = HttpClientHelper.ExecuteInsertPostApiMethod<UserAudit>(baseUrl, "/UserAudit/InsertUserAudit", userAudit, token);
            userAudit = (UserAudit)HttpClientHelper.ExecuteGetApiMethod<UserAudit>(baseUrl, "/UserAudit/GetUserAudit?", "Id=" + userAuditId);
            TempData["UserDate"] = JsonConvert.SerializeObject(userAudit);
           
            return RedirectToAction("Index", "Home", new {Area = TempData.Peek("HeaderRole")});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
