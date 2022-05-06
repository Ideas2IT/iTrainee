using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    public class SubTopicsController : Controller
    {
        ILogger<SubTopicsController> _logger;
        IConfiguration _configuration;

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
        public IActionResult AddEditStream(int id)
        {
            TempData.Remove("StreamId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var stream = HttpClientHelper.ExecuteGetApiMethod<Stream>(baseUrl, "/Stream/Get?", "Id=" + id);
            TempData.Add("StreamId", id);
            return PartialView(stream);
        }

        [HttpPost]
        public IActionResult AddEditStream(Stream stream)
        {
            stream.Id = Convert.ToInt32(TempData["StreamId"]);
            if (stream.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/UpdateStream", stream);
            }
            else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/AddStream", stream);
            }

            return PartialView(stream);
        }

        [HttpDelete]
        public IActionResult DeleteSubTopics(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<SubTopics>(baseUrl, "/SubTopics/DeleteSubTopic?", "Id=" + id);
            return new JsonResult("");
        }
    }
}
