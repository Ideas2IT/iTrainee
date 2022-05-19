using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Route("Shared/[controller]/[action]")]
    [Area("Shared")]
    public class UserTopicsController : Controller
    {
        ILogger<BatchController> _logger;
        IConfiguration _configuration;
        private List<string> getUserIds;
        private List<string> postUserIds;
        private List<string> getStreamIds;
        private List<string> postStreamIds;

        public UserTopicsController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }



        [HttpGet]
        public IActionResult AddEditUserTopics()
        {
            TempData["HeaderRole"] = "Admin";
            UserTopics userTopics = new UserTopics();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            userTopics.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Trainee", "");
            var TopicsList = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");
            var SubTopicsList = (List<SubTopics>)HttpClientHelper.ExecuteGetAllApiMethod<SubTopics>(baseUrl, "/SubTopics/GetAllSubTopics", "");
            foreach (var topics in TopicsList)
            {
                topics.SubTopicsList = SubTopicsList;
            }

            return View(TopicsList);
        }
    }
}
