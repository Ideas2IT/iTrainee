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
            var userTopicsList = HttpClientHelper.ExecuteGetAllApiMethod<UserTopics>(baseUrl, "/UserTopics/GetAllUserTopics?batchId=" + TempData["BatchId"],"", token);
            return View(userTopicsList);
        }


        [HttpGet]
        public IActionResult AddEditUserTopics(string id)
        {
            var token = Convert.ToString(TempData["UserToken"]);
            UserTopics userTopics = new UserTopics();
            TempData["BatchId"] = id;
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
           // userTopics.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsersByBatch?role=Trainee&id=" + id, "", token);
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
        public ActionResult AddEditUserTopics(UserTopics userTopics,string selectedItems)
        {
            var token = Convert.ToString(TempData["UserToken"]);
            StringBuilder subTopicList = new StringBuilder();
            StringBuilder traineeList = new StringBuilder();
            UserTopics newUserTopics = new UserTopics();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<Topics> topics = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "", token);
            List<UserTopics> items = JsonConvert.DeserializeObject<List<UserTopics>>(selectedItems);
            foreach (UserTopics userTopic in items)
            {
                foreach (Topics topic in topics)
                {
                    if (topic.Id.ToString() == userTopic.parent)
                    {
                        newUserTopics.TopicId = topic.Id;
                        foreach (SubTopics subTopic in topic.SubTopic)
                        {
                            if (userTopic.id == subTopic.Id.ToString())
                            {
                                subTopicList.Append(subTopic.Id.ToString()).Append(",");
                            }
                        }
                    }
                }
            }
            foreach (string traineeId in userTopics.SelectedTrainees)
            {
                traineeList.Append(traineeId).Append(",");
            }
            newUserTopics.SelectedSubTopicList = subTopicList.ToString();
            newUserTopics.SelectedTraineeList = traineeList.ToString();
            HttpClientHelper.ExecutePostApiMethod<UserTopics>(baseUrl, "/UserTopics/AddUserTopic", newUserTopics, token);
            return RedirectToAction("ManageUserTopics");
        }
    }
}

