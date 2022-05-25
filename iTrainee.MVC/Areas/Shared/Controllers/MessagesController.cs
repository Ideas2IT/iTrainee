﻿using iTrainee.Models;
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
            TempData.Keep("HeaderRole");
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            return View();
        }

        public IActionResult ManageMessages()
        {
            TempData.Keep("HeaderRole");
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var messages = HttpClientHelper.ExecuteGetAllApiMethod<Messages>(baseUrl, "/Messages/GetMessagesByUserId?", "Id=" + TempData.Peek("UserId"));

            return View(messages);
        }

        public IActionResult ViewAlertDetails(int Id)
        {
            TempData.Keep("HeaderRole");
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var userMessages = HttpClientHelper.ExecuteGetAllApiMethod<UserMessages>(baseUrl, "/Messages/GetUserMessagesByMessageId?", "Id=" + Id);

            return PartialView("ViewAlertDetails", userMessages);
        }

        public IActionResult AddEditAlert(int Id)
        {
            TempData.Keep("HeaderRole");
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var message = HttpClientHelper.ExecuteGetAllApiMethod<Messages>(baseUrl, "/Messages/AddEditMessage?", "Id=" + Id);

            return PartialView("AddEditAlert", message);
        }

        public IActionResult DeleteMessage(int Id)
        {
            TempData.Keep("HeaderRole");
            TempData.Keep("UserFirstName");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Messages>(baseUrl, "/Messages/DeleteMessage?", "Id=" + Id, Convert.ToString(TempData["UserToken"]));
            return RedirectToAction("ManageMessages");
        }
    }
}
