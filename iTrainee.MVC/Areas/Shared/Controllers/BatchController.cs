using iTrainee.Models;
using iTrainee.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTrainee.MVC.Areas.Shared.Controllers
{
    [Route("Shared/[controller]/[action]")]
    [Area("Shared")]
    public class BatchController : Controller
    {
        ILogger<BatchController> _logger;
        IConfiguration _configuration;

        public BatchController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult ManageBatch()
        {
            List<Batch> batchList = new List<Batch>();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var token = TempData["UserToken"].ToString();

            if (TempData["HeaderRole"].ToString() == "Mentor")
            {
                batchList = (List<Batch>)HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "/Batch/GetAllBatches?UserId=" + TempData["UserId"].ToString(), "", token);
            }
            else
            {
                batchList = (List<Batch>)HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "/Batch/GetAllBatches?UserId=0", "", token);
            }

            return View(batchList);
        }

        [HttpGet]
        public IActionResult AddEditBatch(int id)
        {
            var token = Convert.ToString(TempData["UserToken"]);
            TempData.Keep("UserToken");
            Batch batch = new Batch();
            User user = new User();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();

            if (id > 0)
            {
                batch = (Batch)HttpClientHelper.ExecuteGetApiMethod<Batch>(baseUrl, "/Batch/GetBatch?", "Id=" + id, token);
                batch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor", "", token);
                batch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/BatchUser/GetUnassignedTrainees", "", token);
                batch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "", token);
                batch.SelectedMentorIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedMentors?Id=" + id, token);
                batch.SelectedTraineeIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedTrainees?Id=" + id, token);
                batch.SelectedStreamIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchStream/GetSelectedStreams?Id=" + id, token);
                TempData["getUserIds"] = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToList();
                TempData["getStreamIds"] = batch.SelectedStreamIds.ToList();
                foreach (string traineeId in batch.SelectedTraineeIds)
                {
                    user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + traineeId, token);
                    batch.TraineeList.Add(user);
                }
            }
            else
            {
                batch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor", "", token);
                batch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/BatchUser/GetUnassignedTrainees", "", token);
                batch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "", token);
            }

            return View(batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddEditBatch(Batch batch)
        {
            TempData.Keep("UserToken");
            Batch newBatch = new Batch();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            if (ModelState.IsValid)
            {
                var token = Convert.ToString(TempData["UserToken"]);

                if (batch.Id > 0)
                {
                    string[] getUserIdsArray = (string[])TempData["getUserIds"];
                    string[] postUserIdsArray = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToArray();
                    string[] getStreamIdsArray = (string[])TempData["getStreamIds"];
                    string[] postStreamIdsArray = batch.SelectedStreamIds.ToArray();

                    StringBuilder sbSelectedUserIds = new StringBuilder();
                    StringBuilder sbUnselectedUserIds = new StringBuilder();
                    StringBuilder sbSelectedStreamIds = new StringBuilder();
                    StringBuilder sbUnselectedStreamIds = new StringBuilder();

                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/Batch/UpdateBatch", batch, token);

                    foreach (string id in postUserIdsArray.Except(getUserIdsArray))
                    {
                        sbSelectedUserIds.Append(id + ",");
                        batch.StringUserIds = sbSelectedUserIds.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/UpdateBatchUser", batch, token);
                    }

                    foreach (string id in getUserIdsArray.Except(postUserIdsArray))
                    {
                        sbUnselectedUserIds.Append(id + ",");
                        batch.StringUserIds = sbUnselectedUserIds.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/UnassignUserId", batch, token);
                    }
                    foreach (string id in postStreamIdsArray.Except(getStreamIdsArray))
                    {
                        sbSelectedStreamIds.Append(id + ",");
                        batch.StringStreamIds = sbSelectedStreamIds.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/UpdateBatchStream", batch, token);
                    }

                    foreach (string id in getStreamIdsArray.Except(postStreamIdsArray))
                    {
                        sbUnselectedStreamIds.Append(id + ",");
                        batch.StringStreamIds = sbUnselectedStreamIds.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/UnassignStreamId", batch, token);
                    }
                }
                else
                {
                    int batchId = HttpClientHelper.ExecuteInsertPostApiMethod<Batch>(baseUrl, "/Batch/AddBatch", batch, token);
                    batch.Id = batchId;
                    string[] UserIdsArray = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToArray();

                    StringBuilder sbUserIds = new StringBuilder();
                    foreach (string i in UserIdsArray)
                    {
                        sbUserIds.Append(i + ",");
                    }
                    batch.StringUserIds = sbUserIds.ToString();

                    StringBuilder sbstreamIds = new StringBuilder();
                    foreach (string i in batch.SelectedStreamIds)
                    {
                        sbstreamIds.Append(i + ",");
                    }
                    batch.StringStreamIds = sbstreamIds.ToString();

                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/AddBatchUser", batch, token);
                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/AddBatchStream", batch, token);
                }

                return new JsonResult("success");

            }

            return new JsonResult("failed");
        }

        public IActionResult DeleteBatch(int id)
        {
            var token = Convert.ToString(TempData["UserToken"]);
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecuteDeleteApiMethod<Batch>(baseUrl, "/Batch/DeleteBatch?", "Id=" + id, Convert.ToString(TempData["UserToken"]));
            return RedirectToAction("ManageBatch", new { Area = "Shared" });
        }
    }
}
