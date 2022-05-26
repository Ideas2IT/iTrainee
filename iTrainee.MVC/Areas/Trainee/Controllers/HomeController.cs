using iTrainee.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using iTrainee.MVC.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            UserAudit userAudit = (UserAudit)HttpClientHelper.ExecuteGetApiMethod<UserAudit>(baseUrl, "/UserAudit/GetUserAudit?", "Id=" + userId);
            userAudit.AssignedTopicsList = HttpClientHelper.ExecuteGetListApiMethod<Topics>(baseUrl, "/UserTopics/GetUserTopicsByUserId?", "Id=" + userId);
            if (userAudit.AssignedTopicsList.Count() != 0)
            {
                var topicId = userAudit.AssignedTopicsList.First().Id;
                userAudit.AssignedSubTopicsList = HttpClientHelper.ExecuteGetListApiMethod<SubTopics>(baseUrl, "/UserTopics/GetSubTopicsByUserIdAndTopicId?", "userId=" + userId + "&topicId=" + topicId);
            }
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

        public IActionResult UpdateDailyProgress(int userId, int subTopicId, int userAuditId)
        {
            TempData.Keep("UserFirstName");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            DailyProgress dailyProgress = (DailyProgress)HttpClientHelper.ExecuteGetApiMethod<DailyProgress>(baseUrl, "/UserTopics/GetSubTopicOfUser?", "userId=" + userId + "&subTopicId=" + subTopicId);
            dailyProgress.UserAuditId = userAuditId;
            return PartialView(dailyProgress);
        }

        [HttpPost]
        public IActionResult UpdateDailyProgress(DailyProgress dailyProgress)
        {
            TempData.Keep("UserFirstName");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecutePostApiMethod<DailyProgress>(baseUrl, "/UserTopics/UpdateDailyProgress", dailyProgress, Convert.ToString(TempData["UserToken"]));

            return RedirectToAction("Index", new { auditId = dailyProgress.UserAuditId, userId = dailyProgress.UserId });
        }

        public PartialViewResult SubTopicList(int topicId, int userId)
        {
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();

            var SubTopics = HttpClientHelper.ExecuteGetListApiMethod<SubTopics>(baseUrl, "/UserTopics/GetSubTopicsByUserIdAndTopicId?", "userId=" + userId + "&topicId=" + topicId);

            return PartialView(SubTopics);
        }

    }
}
