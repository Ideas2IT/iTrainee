using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Area("Shared")]
    [Route("Shared/[controller]/[action]/{id?}")]
    public class TopicsController : Controller
    {
        IConfiguration _configuration;

        public TopicsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEditTopic(int id)
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            TempData.Remove("TopicId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var topic = HttpClientHelper.ExecuteGetApiMethod<Topics>(baseUrl, "/Topics/Get?", "Id=" + id);
            List<Stream> streamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "");
            TempData.Add("TopicId", id);
            ViewBag.StreamList = new SelectList(streamList, "Id", "Name");

            return PartialView(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditTopic(Topics topic)
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            topic.Id = Convert.ToInt32(TempData["TopicId"]);
            var token = Convert.ToString(TempData["UserToken"]);

            if (topic.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Topics>(baseUrl, "/Topics/UpdateTopic", topic, token);
            }
            else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Topics>(baseUrl, "/Topics/AddTopic", topic, token);
            }

            return RedirectToAction("ManageTopics", "Home", new { Area = "Mentor" });
        }

        public IActionResult DeleteTopic(int id)
        {
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecuteDeleteApiMethod<Topics>(baseUrl, "/Topics/DeleteTopic?", "Id=" + id, Convert.ToString(TempData["UserToken"]));
            return RedirectToAction("ManageTopics", "Home", new { Area = "Mentor" });
        }
    }
}
