using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.Extensions.Configuration;
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

        public IActionResult ManageSubTopics()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var subTopicsList = HttpClientHelper.ExecuteGetAllApiMethod<SubTopics>(baseUrl, "/SubTopics/GetAllSubTopics", "");

            return View(subTopicsList);
        }
        public IActionResult CreateTopic()
        {
            return View();
        }
    }
}
