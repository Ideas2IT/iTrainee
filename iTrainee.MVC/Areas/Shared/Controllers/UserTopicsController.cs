using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            TempData["HeaderRole"] = "Admin";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var userTopicsList = (List<UserTopics>)HttpClientHelper.ExecuteGetAllApiMethod<UserTopics>(baseUrl, "/UserTopics/GetAllUserTopics", "");
            return View(userTopicsList);
        }


        [HttpGet]
        public IActionResult AddEditUserTopics(string id)
        {
            TempData["HeaderRole"] = "Admin";
            UserTopics userTopics = new UserTopics();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            userTopics.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Trainee&id=" + id, "");
            List<Topics> topics = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");
            List<SubTopics> subTopics = (List<SubTopics>)HttpClientHelper.ExecuteGetAllApiMethod<SubTopics>(baseUrl, "/SubTopics/GetAllSubTopics", "");


            //Loop and add the Parent Nodes.
            List<UserTopics> nodes = new List<UserTopics>();

            //Loop and add the Parent Nodes.
            foreach (Topics topic in topics)
            {
                nodes.Add(new UserTopics { id = topic.Id.ToString(), parent = "#", text = topic.Name });
            }

            //Loop and add the Child Nodes.
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
            UserTopics newUserTopics = new UserTopics();
            newUserTopics.SelectedTraineeList = userTopics.SelectedTraineeList;
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<Topics> topics = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");
            List<UserTopics> items = JsonConvert.DeserializeObject<List<UserTopics>>(selectedItems);
            Topics newTopic = new Topics();
            List<SubTopics> newSubTopicList = new List<SubTopics>();
            List<Topics> newTopicList = new List<Topics>();
            foreach (UserTopics userTopic in items)
            {
                newTopic = new Topics();
                foreach (Topics topic in topics)
                {
                    if (topic.Id.ToString() == userTopic.parent)
                    {
                        newTopic.Id = topic.Id;
                        foreach (SubTopics subTopic in topic.SubTopics)
                        {
                            if (userTopic.id == subTopic.Id.ToString())
                            {
                                newSubTopicList.Add(subTopic);
                                newTopic.SubTopics = newSubTopicList;
                            }
                        }
                    }
                }
                newTopicList.Add(newTopic);
                newUserTopics.TopicsList = newTopicList;
            }
            HttpClientHelper.ExecutePostApiMethod<UserTopics>(baseUrl, "/UserTopics/AddUserTopic", newUserTopics, "");
            return null;
        }
    }
}

