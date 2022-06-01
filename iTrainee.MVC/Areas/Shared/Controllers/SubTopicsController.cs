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
            return View();
        }

        [HttpGet]
        public IActionResult AddEditSubTopic(int id, int streamId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var subTopic = HttpClientHelper.ExecuteGetApiMethod<SubTopics>(baseUrl, "/SubTopics/Get?", "Id=" + id);
            List<Stream> streamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "");
            List<Topics> topicsList = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");
            TempData["SubTopicId"] = id;
            ViewBag.StreamList = new SelectList(streamList, "Id", "Name");
            ViewBag.TopicsList = new SelectList(topicsList, "Id", "Name");

            return PartialView(subTopic);
        }

        public IActionResult GetTopicList(int streamId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<Topics> topicsList = (List<Topics>)HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetTopicsByStreamId?", "streamId=" + streamId);
            return new JsonResult(new { data = topicsList });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditSubTopic(SubTopics subTopic)
        {
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
            return RedirectToAction("ManageSubTopics", "Home", new { Area = "Mentor" });
        }
        [HttpPost]
        public IActionResult DeleteSubTopic(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<SubTopics>(baseUrl, "/SubTopics/DeleteSubTopic?", "Id=" + id, "");
            return new JsonResult("");
        }
    }
}
