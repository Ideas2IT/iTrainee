using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
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
        public IActionResult AddEditSubTopic(int id)
        {
            TempData.Remove("SubTopicsId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var subTopics = HttpClientHelper.ExecuteGetApiMethod<SubTopics>(baseUrl, "/SubTopics/Get?", "Id=" + id);
            TempData.Add("SubTopicsId", id);
            return PartialView(subTopics);
        }

        [HttpPost]
        public IActionResult AddEditSubTopic(SubTopics subTopics)
        {
            subTopics.Id = Convert.ToInt32(TempData["SubTopicsId"]);
            if (subTopics.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<SubTopics>(baseUrl, "/SubTopics/UpdateSubTopics", subTopics, "");
            }
            else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<SubTopics>(baseUrl, "/Stream/AddSubTopics", subTopics, "");
            }

            return PartialView(subTopics);
        }

        [HttpDelete]
        public IActionResult DeleteSubTopics(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<SubTopics>(baseUrl, "/SubTopics/DeleteSubTopic?", "Id=" + id, "");
            return new JsonResult("");
        }
    }
}
