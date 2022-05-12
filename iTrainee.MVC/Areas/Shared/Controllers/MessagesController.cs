using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Area("Shared")]
    [Route("Shared/[controller]/[action]/{id?}")]
    public class MessagesController : Controller
    {
        ILogger<MessagesController> _logger;
        IConfiguration _configuration;

        public MessagesController(ILogger<MessagesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageMessages(int Id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var batchList = HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "//GetUserMessages", "");

            return View(batchList);
        }

        public IActionResult GetMessages()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            //var result = HttpClientHelper.ExecuteGetAllApiMethod<Messages>(baseUrl, "");
            return new JsonResult("");
        }

        //public IActionResult AddStream(Messages messages)
        //{
        //    var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
        //    var result = HttpClientHelper.ExecutePostApiMethod<Messages>(baseUrl, "", messages);
        //    return new JsonResult("");
        //}

    }
}
