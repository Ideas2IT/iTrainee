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
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var topicsList = HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");

            return View(topicsList);
        }

        public IActionResult ManageStreams()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var streamList = HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllStreams", "");
            
            return View(streamList);
        }

        public IActionResult CreateTopic()
        {
            return View();
        }
    }
}
