using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Route("Shared/[controller]/[action]")]
    [Area("Shared")]
    public class BatchController : Controller
    {
        ILogger<BatchController> _logger;
        IConfiguration _configuration;
        private List<string> getUserIds;
        private List<string> postUserIds;
        private List<string> getStreamIds;
        private List<string> postStreamIds;

        public BatchController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult ManageBatch()
        {
            TempData["HeaderRole"] = "Mentor";
            TempData["HeaderRole"] = "Admin";
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var batchList = HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "/Batch/GetAllBatches", "");

            return View(batchList);
        }

        [HttpGet]
        public IActionResult AddEditBatch(int id)
        {
            TempData["HeaderRole"] = "Admin";
            Batch batch = new Batch();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            batch = (Batch)HttpClientHelper.ExecuteGetApiMethod<Batch>(baseUrl, "/Batch/Get?", "Id=" + id);
            batch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor", "");
            batch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Trainee", "");
            batch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "");

            if (id > 0)
            {
                batch.SelectedMentorIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedMentors?Id=" + id);
                batch.SelectedTraineeIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedTrainees?Id=" + id);
                batch.SelectedStreamIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchStream/GetSelectedStreams?Id=" + id);
                TempData["getUserIds"] = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToList();
                TempData["getStreamIds"] = batch.SelectedStreamIds.ToList();
            }
            
            return View(batch);
        }

        [HttpPost]
        public IActionResult AddEditBatch(Batch batch)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            
            if (batch.Id > 0)
            {
                string[] getUserIdsArray = (string[])TempData["getUserIds"];
                string[] postUserIdsArray = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToArray();
                string[] getStreamIdsArray = (string[])TempData["getStreamIds"];
                string[] postStreamIdsArray = batch.SelectedStreamIds.ToArray();
                getUserIds = getUserIdsArray.ToList();
                postUserIds = postUserIdsArray.ToList();
                getStreamIds = getStreamIdsArray.ToList();
                postStreamIds = postStreamIdsArray.ToList();
                foreach (string unSelectedId in getUserIds)
                {
                    foreach(string selectedId in postUserIds)
                    {
                        if(selectedId == unSelectedId)
                        {
                            postUserIdsArray = postUserIdsArray.Where(val => val != selectedId).ToArray();
                        }
                        else
                        {
                            getUserIdsArray = getUserIdsArray.Where(val => val != selectedId).ToArray();
                        }
                    }
                }
                foreach (string unSelectedId in getStreamIds)
                {
                    foreach (string selectedId in postStreamIds)
                    {
                        if (selectedId == unSelectedId)
                        {
                            postStreamIdsArray = postStreamIdsArray.Where(val => val != selectedId).ToArray();
                        }
                        else
                        {
                            getStreamIdsArray = getStreamIdsArray.Where(val => val != selectedId).ToArray();
                        }
                    }
                }
                HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/Batch/UpdateBatch", batch);

                if(postUserIdsArray.Length != 0)
                {
                    batch.StringUserIds = string.Join(",", postUserIdsArray);
                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/UpdateBatchUser", batch);
                }
                if(getUserIdsArray.Length != 0)
                {
                    batch.StringUserIds = string.Join(",", getUserIdsArray);
                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/UnassignUserId", batch);
                }
                if (postStreamIdsArray.Length != 0)
                {
                    batch.StringStreamIds = string.Join(",", postStreamIdsArray);
                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/UpdateBatchStream", batch);
                }
                if (getStreamIdsArray.Length != 0)
                {
                    batch.StringStreamIds = string.Join(",", getStreamIdsArray);
                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/UnassignStreamId", batch);
                }

                getUserIds = null;
                postUserIds = null;
                getStreamIds = null;
                postStreamIds = null;
            } 
            else
            {
                int batchId = HttpClientHelper.ExecuteInsertPostApiMethod<Batch>(baseUrl, "/Batch/AddBatch", batch);
                batch.Id = batchId;
                string[] UserIdsArray = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToArray();
                batch.StringUserIds = string.Join(",", UserIdsArray);
                batch.StringStreamIds = string.Join(",", batch.SelectedStreamIds);
                HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/AddBatchUser", batch);
                HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/AddBatchStream", batch);
            }

            return RedirectToAction("ManageBatch", new { Area="Shared" });
        }

        public IActionResult DeleteBatch(int id)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecuteDeleteApiMethod<Batch>(baseUrl, "/Batch/DeleteBatch?", "Id=" + id);
            return RedirectToAction("ManageBatch", new { Area = "Shared" });
        }
    }
}
