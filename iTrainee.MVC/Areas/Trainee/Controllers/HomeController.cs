using iTrainee.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using iTrainee.MVC.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace iTrainee.MVC.Areas.Trainee.Controllers
{
    [Area("Trainee")]
    [Route("Trainee/[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            UserAudit user = JsonConvert.DeserializeObject<UserAudit>(TempData["UserDate"].ToString());
            TempData["HeaderRole"] = "Trainee";
            TempData.Peek("UserToken");
            return View(user);
        }
    }
}
