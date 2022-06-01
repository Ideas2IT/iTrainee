using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace iTrainee.MVC.Areas.Admin.Controllers
{
    [Produces("application/json")]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;
        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUser(string role, int userId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
             List<User> user = new List<User>();
            if (Convert.ToString(TempData["HeaderRole"]) == "Admin")
            {
                user = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?", "role=" + role);
            }
           else
            {
                string[] batchIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/User/GetAssignedBatchIds?userId=" + userId);
                foreach (string id in batchIds)
                {
                    user.AddRange((List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetAssignedTrainees?batchId=" + Convert.ToInt32(id), ""));
                }
            }
                ViewBag.Role = role;
            TempData["Role"] = role;

            return View(user);
        }

        public IActionResult SaveUser(int id)
        {
            var user = new User();

            if ((Convert.ToString(TempData["Role"]) == "Admin"))
            {
                user.IsAdmin = true;
            }
            else if ((Convert.ToString(TempData["Role"]) == "Mentor"))
            {
                user.IsMentor = true;
            }
            else if ((Convert.ToString(TempData["Role"]) == "Trainee"))
            {
                user.IsTrainee = true;
            }

            if (0 < id)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + id);
                TempData["UserId"] = id;
            }

            return PartialView(user);
        }

        [HttpPost]
        [System.Web.Mvc.HandleError]
        public IActionResult SaveUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (0 < user.Id)
                {
                    user.Id = Convert.ToInt32(TempData["UserId"]);
                }

                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<User>(baseUrl, "/User/SaveUser", user, TempData["UserToken"].ToString());

                return RedirectToAction("ManageUser", "Home", new { role = Convert.ToString(TempData["Role"]) });
            } 

            return RedirectToAction("SaveUser", new { id = user.Id });
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<User>(baseUrl, "/User/DeleteUser?", "Id=" + id, TempData["UserToken"].ToString());
            return RedirectToAction("ManageUser", "Home", new { role = TempData["Role"].ToString() });
        }
    }
}
