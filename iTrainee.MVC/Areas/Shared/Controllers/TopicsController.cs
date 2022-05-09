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
            TempData.Remove("TopicId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var topic = HttpClientHelper.ExecuteGetApiMethod<Topics>(baseUrl, "/Topics/Get?", "Id=" + id);
            List<Stream> streamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams");
            TempData.Add("TopicId", id);
            ViewBag.StreamList = new SelectList(streamList, "Id", "Name");

            return PartialView(topic);
        }

        [HttpPost]
        public IActionResult AddEditTopic(Topics topic)
        {
            topic.Id = Convert.ToInt32(TempData["TopicId"]);
            if (topic.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Topics>(baseUrl, "/Topics/UpdateTopic", topic);
            }
            else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Topics>(baseUrl, "/Topics/AddTopic", topic);
            }

            return PartialView(topic);
        }

        public IActionResult DeleteTopic(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Topics>(baseUrl, "/Topics/DeleteTopic?", "Id=" + id);
            return new JsonResult("");
        }
    }
}
