using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

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

        public JsonResult CheckUsernameAvailability(string userName)
        {
            TempData.Keep("UserToken");
            var token = Convert.ToString(TempData["UserToken"]);
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            User user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUserByUserName?", "UserName=" + userName, token);
           
            if(user.UserName != null)
            {
                return Json(1);
            } 
            return Json(0);
        }

        public IActionResult ManageUser(string role, int userId, string searchString)
        {
            var token = Convert.ToString(TempData["UserToken"]);
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            List<User> user = new List<User>();
            
            if (Convert.ToString(TempData["HeaderRole"]) == "Admin")
            {
                user = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?", "role=" + role, token);
            }
            else
            {
                string[] batchIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/User/GetAssignedBatchIds?userId=" + userId, token);
                foreach (string id in batchIds)
                {
                    user.AddRange((List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetAssignedTrainees?batchId=" + Convert.ToInt32(id), "", token));
                }
            }
            ViewBag.Role = role;
            TempData["Role"] = role;

            return View(user);
        }

        public PartialViewResult SaveUser(int id)
        {
            TempData.Keep("UserToken");
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
                user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + id, Convert.ToString(TempData["UserToken"]));
            }

            return PartialView(user);
        }

        [HttpPost]
        [System.Web.Mvc.HandleError]
        [ValidateAntiForgeryToken()]
        public IActionResult SaveUser(User user)
        {
            TempData.Keep("UserToken");

            if (ModelState.IsValid)
            {
                var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
                HttpClientHelper.ExecutePostApiMethod<User>(baseUrl, "/User/SaveUser", user, TempData["UserToken"].ToString());
            }
            return RedirectToAction("ManageUser", "Home", new { role = Convert.ToString(TempData["Role"]) });
        }

        [HttpDelete]
        public JsonResult DeleteUser(int id)
        {
            TempData.Keep("UserToken");
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var result = HttpClientHelper.ExecuteDeleteApiMethod<User>(baseUrl, "/User/DeleteUser?", "Id=" + id, TempData["UserToken"].ToString());
            return new JsonResult("success");
        }
    }
}
