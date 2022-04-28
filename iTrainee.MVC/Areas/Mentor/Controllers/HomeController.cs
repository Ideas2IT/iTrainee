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

namespace iTrainee.MVC.Areas.Mentor.Controllers
{

    [Area("Mentor")]
    public class HomeController : Controller
    {
        readonly Uri baseAddress = new Uri("http://localhost:62154");
        readonly HttpClient client;

        public HomeController()
        {
            client = new HttpClient
            {
                BaseAddress = baseAddress
            };
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

        public IActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTopic(Topics topic)
        {

            string data = JsonConvert.SerializeObject(topic);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Mentor", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }



            return View();
        }
    }
}
