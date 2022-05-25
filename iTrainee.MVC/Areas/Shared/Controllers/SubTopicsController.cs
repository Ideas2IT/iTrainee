using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Area("Shared")]
    [Route("Shared/[controller]/[action]/{id?}")]
    public class SubTopicsController : Controller
    {
        private ILogger<SubTopicsController> _logger;
        private IConfiguration _configuration;

        public SubTopicsController(ILogger<SubTopicsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            TempData.Keep("UserId");
            return View();
        }

        [HttpGet]
        public IActionResult AddEditSubTopic(int id)
        {
            TempData.Keep("UserId");
            TempData.Remove("SubTopicId");
            TempData.Keep("UserFirstName");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var subTopic = HttpClientHelper.ExecuteGetApiMethod<SubTopics>(baseUrl, "/SubTopics/Get?", "Id=" + id);
            List<Topics> topicsList = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");
            TempData.Add("SubTopicId", id);
            ViewBag.TopicsList = new SelectList(topicsList, "Id", "Name");

            return PartialView(subTopic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditSubTopic(SubTopics subTopic)
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            subTopic.Id = Convert.ToInt32(TempData["SubTopicId"]);
            if (subTopic.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<SubTopics>(baseUrl, "/SubTopics/UpdateSubTopic", subTopic, "");
            }
            else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<SubTopics>(baseUrl, "/SubTopics/AddSubTopic", subTopic, "");
            }
            return PartialView(subTopic);
        }

        [HttpPost]
        public IActionResult DeleteSubTopic(int id)
        {
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<SubTopics>(baseUrl, "/SubTopics/DeleteSubTopic?", "Id=" + id, "");
            return new JsonResult("");
        }
    }
}
