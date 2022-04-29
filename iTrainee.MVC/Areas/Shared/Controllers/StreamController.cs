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

        public IActionResult AddStream(Stream stream)
        {
            var parameters = new List<SqlParameter>();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "", parameters);
            return new JsonResult("");
        }

        public IActionResult DeleteStream(int id)
        {
            //var parameters = new List<SqlParameter>();
            //parameters.Add(new SqlParameter
            //{
            //    ParameterName = "Id",
            //    Value = id
            //});
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Stream>(baseUrl, "/Stream/DeleteStream/ "+id);
            return new JsonResult("");
        }
    }
}
