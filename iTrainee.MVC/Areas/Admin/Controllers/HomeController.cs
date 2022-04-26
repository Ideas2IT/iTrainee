using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace iTrainee.MVC.Admin.Controllers
{
    public class TraineeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
