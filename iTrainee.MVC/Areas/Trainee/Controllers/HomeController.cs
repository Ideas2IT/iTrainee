using iTrainee.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using iTrainee.MVC.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace iTrainee.MVC.Areas.Trainee.Controllers
{
    [Area("Trainee")]
    [Route("Trainee/[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        public IActionResult Index(int auditId, int userId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            UserAudit userAudit = (UserAudit)HttpClientHelper.ExecuteGetApiMethod<UserAudit>(baseUrl, "/UserAudit/GetUserAudit?", "Id=" + userId);
            if (auditId == 0)
            {
                TempData["HeaderRole"] = "Mentor";
                TempData["FirstName"] = "MentorName";
            }
            else
            {
                TempData["HeaderRole"] = "Trainee";
                TempData["FirstName"] = "TraineeName";
            }
            UserAudit userAudit = (UserAudit)HttpClientHelper.ExecuteGetApiMethod<UserAudit>(baseUrl, "/UserAudit/GetUserAudit?", "Id=" + id);
            userAudit.AssignedTopicsList = (List<Topics>)HttpClientHelper.ExecuteGetApiMethod<Topics>(baseUrl, "/UserTopics/GetUserTopicsByUserId?", "Id=" + id);
            TempData["HeaderRole"] = "Trainee";
            TempData.Keep("HeaderRole");
            TempData.Peek("UserToken");
            return View(userAudit);
        }
    }
}
