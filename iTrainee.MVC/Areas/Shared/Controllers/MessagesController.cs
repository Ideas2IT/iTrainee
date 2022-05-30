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
using System.Text;
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
            TempData.Keep("UserId");
            return View();
        }

        public IActionResult ManageMessages()
        {
            TempData.Keep("HeaderRole");
            TempData.Keep("UserId");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<Messages> messages = (List<Messages>)HttpClientHelper.ExecuteGetAllApiMethod<Messages>(baseUrl, "/Messages/GetMessagesByUserId?", "Id=" + TempData.Peek("UserId"));

            return View(messages);
        }

        public IActionResult ViewAlertDetails(int Id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var userMessages = HttpClientHelper.ExecuteGetAllApiMethod<UserMessages>(baseUrl, "/Messages/GetUserMessagesByMessageId?", "Id=" + Id);

            return PartialView("ViewAlertDetails", userMessages);
        }

        [HttpGet]
        public IActionResult AddEditMessages(int Id)
        {
            Messages message = new Messages();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            
            if (Id > 0)
            {
                message = (Messages)HttpClientHelper.ExecuteGetApiMethod<Messages>(baseUrl, "/Messages/GetMessageById?", "Id=" + Id);
                message.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/UserMessages/GetTrainees?", "");
                message.SelectedTraineeIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/UserMessages/GetSelectedTrainees?Id=" + Id);
            }
            else
            {
                message.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/UserMessages/GetTrainees?", "");
                message.FromId = (int)TempData["UserId"];
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditMessages(Messages alert)
        {
            TempData.Keep("HeaderRole");
            var token = Convert.ToString(TempData["UserToken"]);
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();

            if (alert.Id > 0)
            {

            }
            else
            {
                int batchId = HttpClientHelper.ExecuteInsertPostApiMethod<Messages>(baseUrl, "/Messages/AddMessage", alert, token);
                alert.Id = batchId;

                StringBuilder sbUserIds = new StringBuilder();
                foreach (string i in alert.SelectedTraineeIds)
                {
                    sbUserIds.Append(i + ",");
                }
                alert.TraineesIdsString = sbUserIds.ToString();

                HttpClientHelper.ExecutePostApiMethod<Messages>(baseUrl, "/UserMessages/AddUserMessage", alert, token);
            }
            return RedirectToAction("ManageMessages", new { Area = "Shared" });
        }

        public IActionResult DeleteMessage(int Id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<Messages>(baseUrl, "/Messages/DeleteMessage?", "Id=" + Id, Convert.ToString(TempData["UserToken"]));
            return RedirectToAction("ManageMessages");
        }
    }
}
