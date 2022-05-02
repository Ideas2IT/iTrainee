using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    public class TopicsController : Controller
    {
        IConfiguration _configuration;

        public TopicsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTopics()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteGetAllApiMethod<Topics>(baseUrl, "", "");
            return new JsonResult("");
        }

        public IActionResult AddTopic(Topics topic)
        {
            var parameters = new List<SqlParameter>();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecutePostApiMethod<Topics>(baseUrl, "", topic);
            return new JsonResult("");
        }
    }
}
