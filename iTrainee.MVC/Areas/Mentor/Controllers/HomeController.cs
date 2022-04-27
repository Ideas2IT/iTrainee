using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Text;

namespace iTrainee.MVC.Areas.Mentor.Controllers
{

    [Area("Mentor")]
    public class HomeController : Controller
    {
        readonly Uri baseAddress = new Uri("http://localhost:62154/");
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
            return View();
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
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/values", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }



            return View();
        }
    }
}
