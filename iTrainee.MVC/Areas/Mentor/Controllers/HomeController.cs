using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Text;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using iTrainee.Services.Interfaces;
using iTrainee.Services.Implementations;
using iTrainee.MVC.Helpers;
using Microsoft.Extensions.Configuration;
using iTrainee.MVC.Areas.Shared.Controllers;
using Microsoft.Extensions.Logging;

namespace iTrainee.MVC.Areas.Mentor.Controllers
{
    [Area("Mentor")]
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;
        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageTopics()
        {
            //List<Topics> topicsList = new List<Topics>();
            //HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Mentor").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    string data = response.Content.ReadAsStringAsync().Result;
            //    topicsList = JsonConvert.DeserializeObject<List<Topics>>(data);
            //}

            List<Topics> topicsList = new List<Topics>();
            Topics topic = new Topics();
            topic.Id = 1;
            topic.Name = "Test";
            topic.StreamId = 1;
            topic.ReferenceURL = "www.webAPI.com";
            topic.IsActive = true;
            topic.InsertedBy = "Admin";
            topic.InsertedOn = DateTime.Now;
            topic.UpdatedBy = "None";
            topic.UpdatedOn = DateTime.Now;

            topicsList.Add(topic);

   //         IEnumerable<Stream> streamList = new List<Stream>();
   //         Stream stream = new Stream();
   //         stream.Name = "abc1";
   //         stream.Id = 1;
   //         streamList;
   //dynamic mymodel = new ExpandoObject();
   //mymodel.TopicsList = topicsList;
   //mymodel.StreamList = streamList;
        ITopicsService topicService = new TopicsService();
        ViewBag.StreamList = new SelectList(topicService.GetAllTopics(), "Id" , "Name");
            return View(topicsList);
        }

        public IActionResult ManageStreams()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllStreams", "");

            return View(result);
        }

        public IActionResult CreateTopic()
        {
            return View();
        }
    }
}
