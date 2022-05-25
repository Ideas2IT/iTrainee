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
            UserAudit userAudit = new UserAudit();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            userAudit = (UserAudit)HttpClientHelper.ExecuteGetApiMethod<UserAudit>(baseUrl, "/UserAudit/GetUserAudit?", "Id=" + userId);

               userAudit.AssignedTopicsList = HttpClientHelper.ExecuteGetListApiMethod<Topics>(baseUrl, "/UserTopics/GetUserTopicsByUserId?", "Id=" + userId);
              userAudit.AssignedSubTopicsList = HttpClientHelper.ExecuteGetListApiMethod<SubTopics>(baseUrl, "/UserTopics/GetSubTopicsByUserId?", "Id=" + userId);
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

            TempData.Keep("HeaderRole");
            TempData.Peek("UserToken");

            return View(userAudit);
        }

        public IActionResult UpdateDailyProgress(int userId, int subTopicId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            DailyProgress dailyProgress = (DailyProgress)HttpClientHelper.ExecuteGetApiMethod<DailyProgress>(baseUrl, "/UserTopics/GetSubTopicOfUser?", "userId=" + userId + "&subTopicId=" + subTopicId);
            
            return PartialView(dailyProgress);
        }

        [HttpPost]
        public IActionResult UpdateDailyProgress(DailyProgress dailyProgress)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecutePostApiMethod<DailyProgress>(baseUrl, "/UserTopics/UpdateDailyProgress", dailyProgress, Convert.ToString(TempData["UserToken"]));
            return PartialView();
        }
    }
}
