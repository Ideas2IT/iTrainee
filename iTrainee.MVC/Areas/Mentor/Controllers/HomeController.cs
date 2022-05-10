using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace iTrainee.MVC.Areas.Mentor.Controllers
{
    [Area("Mentor")]
    [Route("Mentor/[controller]/[action]/{id?}")]
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
            TempData["HeaderRole"] = "Mentor";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<Batch> batchList = (List<Batch>)HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "/Batch/GetAllBatches", "");
            foreach (var batch in batchList)
            {
                batch.SelectedTraineeIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedTrainees?Id=" + batch.Id);
            }
            return View(batchList);
        }

        public IActionResult ManageTopics()
        {
            TempData["HeaderRole"] = "Mentor";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var topicsList = HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "/Topics/GetAllTopics", "");

            return View(topicsList);
        }

        public IActionResult ManageStreams()
        {
            TempData["HeaderRole"] = "Mentor";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var streamList = HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllStreams", "");
            
            return View(streamList);
        }

        public IActionResult ManageSubTopics()
        {
            TempData["HeaderRole"] = "Mentor";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var subTopicsList = HttpClientHelper.ExecuteGetAllApiMethod<SubTopics>(baseUrl, "/SubTopics/GetAllSubTopics");

            return View(subTopicsList);
        }
        public IActionResult CreateTopic()
        {
            return View();
        }
    }
}
