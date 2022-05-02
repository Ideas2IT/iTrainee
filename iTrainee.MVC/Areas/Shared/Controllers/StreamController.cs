using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult GetStreams()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "", "");
            return new JsonResult("");
        }

        [HttpGet]
        public IActionResult AddEditStream(int id)
        {

            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            Stream stream = (Stream)HttpClientHelper.ExecuteGetApiMethod<Stream>(baseUrl, "/Stream/Get?", "Id="+id);
            return PartialView(stream);
        }

        [HttpPost]
        public IActionResult AddEditStream(Stream stream)
        {
            if(stream.Id > 0)
            { 
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/UpdateStream", stream);
            } else
            {

            }


            return PartialView(stream);
        }

        [HttpPost]
        public IActionResult AddStream(Stream stream)
        { 

            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/AddStream", stream);
            return new JsonResult("");
        }

        public IActionResult DeleteStream(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Stream>(baseUrl, "/Stream/DeleteStream?", "Id="+id);
            return new JsonResult("");
        }
    }
}
