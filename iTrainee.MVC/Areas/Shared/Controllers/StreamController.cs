using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Area("Shared")]
    public class StreamController : Controller
    {
        ILogger<StreamController> _logger;
        IConfiguration _configuration;

        public StreamController(ILogger<StreamController> logger, IConfiguration configuration)
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
            TempData.Keep("UserId");
            TempData.Remove("StreamId");
            TempData.Keep("UserFirstName");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var stream = HttpClientHelper.ExecuteGetApiMethod<Stream>(baseUrl, "/Stream/Get?", "Id="+id);
            TempData.Add("StreamId", id);
            return PartialView(stream);
        }

        [HttpPost]
        public IActionResult AddEditStream(Stream stream)
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            var token = Convert.ToString(TempData["UserToken"]);
            stream.Id = Convert.ToInt32(TempData["StreamId"]);

            if (stream.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/UpdateStream", stream, token);
            } else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/AddStream", stream, token);
            }

            return RedirectToAction("ManageStreams", "Home", new { Area = "Mentor" });
        }

        public IActionResult DeleteStream(int id)
        {
            TempData.Keep("UserId");
            TempData.Keep("UserFirstName");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Stream>(baseUrl, "/Stream/DeleteStream?", "Id="+id, Convert.ToString(TempData["UserToken"]));
            return RedirectToAction("ManageStreams", "Home", new { Area = "Mentor" });
        }
    }
}
