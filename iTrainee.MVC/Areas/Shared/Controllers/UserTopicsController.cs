using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Route("Shared/[controller]/[action]")]
    [Area("Shared")]
    public class UserTopicsController : Controller
    {
        ILogger<BatchController> _logger;
        IConfiguration _configuration;
        private int i;

        public UserTopicsController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult ManageUserTopics()
        {
            var token = Convert.ToString(TempData["UserToken"]);
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var userTopicsList = HttpClientHelper.ExecuteGetAllApiMethod<UserTopics>(baseUrl, "/UserTopics/GetAllUserTopics?batchId=" + TempData["BatchId"], "", token);
            return View(userTopicsList);
        }

        [HttpGet]
        public IActionResult AddEditUserTopics(string id)
        {
            TempData.Keep("UserToken");
            var token = Convert.ToString(TempData["UserToken"]);
            UserTopics userTopics = new UserTopics();
            TempData["BatchId"] = id;
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            userTopics.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsersByBatch?role=Trainee&id=" + id, "", token);
            List<Topics> topics = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "", token);
            List<SubTopics> subTopics = (List<SubTopics>)HttpClientHelper.ExecuteGetAllApiMethod<SubTopics>(baseUrl, "/SubTopics/GetAllSubTopics", "", token);
            List<UserTopics> nodes = new List<UserTopics>();
            foreach (Topics topic in topics)
            {
                nodes.Add(new UserTopics { id = topic.Id.ToString(), parent = "#", text = topic.Name });
            }
            foreach (SubTopics subTopic in subTopics)
            {
                nodes.Add(new UserTopics { id = subTopic.TopicId.ToString() + "-" + subTopic.Id.ToString(), parent = subTopic.TopicId.ToString(), text = subTopic.Name });
            }

            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View(userTopics);
        }

        [HttpPost]
        public JsonResult AddEditUserTopics(UserTopics userTopics, string selectedItems)
        {
            TempData.Keep("UserToken");
            var token = Convert.ToString(TempData["UserToken"]);
            UserTopics newUserTopics = new UserTopics();
            List<Topics> selectedTopics = new List<Topics>();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<Topics> topics = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "", token);
            List<UserTopics> items = JsonConvert.DeserializeObject<List<UserTopics>>(selectedItems);
           
            foreach (Topics topic in topics)
            {
                List<SubTopics> selectedSubTopics = new List<SubTopics>();
                foreach (UserTopics userTopic in items)
                {
                    if (topic.Id.ToString() == userTopic.parent)
                    { 
                        foreach (SubTopics subTopic in topic.SubTopic)
                        {
                            if (userTopic.id == subTopic.Id.ToString())
                            {
                                selectedSubTopics.Add(subTopic);
                            }
                        }
                    }
                }
                if (selectedSubTopics.Count != 0)
                {
                    topic.SubTopic = selectedSubTopics;
                    selectedTopics.Add(topic);
                }
            }
            newUserTopics.TopicList = selectedTopics;
            newUserTopics.SelectedTraineeList = string.Join(",", userTopics.SelectedTrainees);
            HttpClientHelper.ExecutePostApiMethod<UserTopics>(baseUrl, "/UserTopics/AddUserTopic", newUserTopics, token);
            return new JsonResult("success");
        }
    }
}

