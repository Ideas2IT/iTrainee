﻿using iTrainee.Models;
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
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var stream = HttpClientHelper.ExecuteGetApiMethod<Stream>(baseUrl, "/Stream/Get?", "Id="+id, Convert.ToString(TempData["UserToken"]));
            TempData["StreamId"] = id;
            return PartialView(stream);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult AddEditStream(Stream stream)
        {
            stream.Id = Convert.ToInt32(TempData["StreamId"]);

            if (stream.Id > 0)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/UpdateStream", stream, TempData["UserToken"].ToString());
            } else
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<Stream>(baseUrl, "/Stream/AddStream", stream, TempData["UserToken"].ToString());
            }

            return RedirectToAction("ManageStreams", "Home", new { Area = "Mentor" });
        }

        [HttpDelete]
        public JsonResult DeleteStream(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Stream>(baseUrl, "/Stream/DeleteStream?", "Id="+id, TempData["UserToken"].ToString());
            TempData.Keep("UserToken");
            return new JsonResult("success");
        }
    }
}
