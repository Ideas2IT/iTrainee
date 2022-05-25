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

        public UserTopicsController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult ManageUserTopics()
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            TempData["HeaderRole"] = "Admin";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var userTopicsList = (List<UserTopics>)HttpClientHelper.ExecuteGetAllApiMethod<UserTopics>(baseUrl, "/UserTopics/GetAllUserTopics", "");
            return View(userTopicsList);
        }


        [HttpGet]
        public IActionResult AddEditUserTopics()
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            TempData.Keep("HeaderRole");
            UserTopics userTopics = new UserTopics();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            userTopics.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Trainee", "");
            userTopics.TopicsList = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");
            return View(userTopics);
        }
    }
}
