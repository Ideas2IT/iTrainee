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
        private List<string> getUserIds;
        private List<string> postUserIds;
        private List<string> getStreamIds;
        private List<string> postStreamIds;

        public BatchController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult ManageBatch(int userId)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var batchList = HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "/Batch/GetAllBatches?UserId=" + userId, "", Convert.ToString(TempData["UserToken"]));

            return View(batchList);
        }

        [HttpGet]
        public IActionResult AddEditBatch(int id)
        {
            Batch batch = new Batch();
            User user = new User();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();

            if (id > 0)
            {
                batch = (Batch)HttpClientHelper.ExecuteGetApiMethod<Batch>(baseUrl, "/Batch/GetBatch?", "Id=" + id, Convert.ToString(TempData["UserToken"]));
                batch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor", "", Convert.ToString(TempData["UserToken"]));
                batch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/BatchUser/GetUnassignedTrainees", "", Convert.ToString(TempData["UserToken"]));
                batch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "", Convert.ToString(TempData["UserToken"]));
                batch.SelectedMentorIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedMentors?Id=" + id, Convert.ToString(TempData["UserToken"]));
                batch.SelectedTraineeIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedTrainees?Id=" + id, Convert.ToString(TempData["UserToken"]));
                batch.SelectedStreamIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchStream/GetSelectedStreams?Id=" + id, Convert.ToString(TempData["UserToken"]));
                TempData["getUserIds"] = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToList();
                TempData["getStreamIds"] = batch.SelectedStreamIds.ToList();
                foreach (string traineeId in batch.SelectedTraineeIds)
                {
                    user = (User)HttpClientHelper.ExecuteGetApiMethod<User>(baseUrl, "/User/GetUser?", "Id=" + traineeId, Convert.ToString(TempData["UserToken"]));
                    batch.TraineeList.Add(user);
                }
            }
            else
            {
                batch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor", "", Convert.ToString(TempData["UserToken"]));
                batch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/BatchUser/GetUnassignedTrainees", "", Convert.ToString(TempData["UserToken"]));
                batch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "", Convert.ToString(TempData["UserToken"]));
            }

            return View(batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.HandleError]
        public IActionResult AddEditBatch(Batch batch)
        {
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
                    getUserIds = getUserIdsArray.ToList();
                    postUserIds = postUserIdsArray.ToList();
                    getStreamIds = getStreamIdsArray.ToList();
                    postStreamIds = postStreamIdsArray.ToList();
                    foreach (string unSelectedId in getUserIds)
                    {
                        foreach (string selectedId in postUserIds)
                        {
                            if (selectedId == unSelectedId)
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
                                getStreamIdsArray = getStreamIdsArray.Where(val => val != selectedId).ToArray();
                            }
                        }
                    }
                    HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/Batch/UpdateBatch", batch, token);

                    if (postUserIdsArray.Length != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (string i in postUserIdsArray)
                        {
                            sb.Append(i + ",");
                        }
                        batch.StringUserIds = sb.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/UpdateBatchUser", batch, token);
                    }
                    if (getUserIdsArray.Length != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (string i in getUserIdsArray)
                        {
                            sb.Append(i + ",");
                        }
                        batch.StringUserIds = sb.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/UnassignUserId", batch, token);
                    }
                    if (postStreamIdsArray.Length != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (string i in postStreamIdsArray)
                        {
                            sb.Append(i + ",");
                        }
                        batch.StringStreamIds = sb.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/UpdateBatchStream", batch, token);
                    }
                    if (getStreamIdsArray.Length != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (string i in getStreamIdsArray)
                        {
                            sb.Append(i + ",");
                        }
                        batch.StringStreamIds = sb.ToString();
                        HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchStream/UnassignStreamId", batch, token);
                    }

                    getUserIds = null;
                    postUserIds = null;
                    getStreamIds = null;
                    postStreamIds = null;
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

                return RedirectToAction("ManageBatch", new { Area = "Shared" });
            } else
            {
                newBatch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor", "", Convert.ToString(TempData["UserToken"]));
                newBatch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/BatchUser/GetUnassignedTrainees", "", Convert.ToString(TempData["UserToken"]));
                newBatch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams", "", Convert.ToString(TempData["UserToken"]));
            }

            return View(newBatch);
        }

        public IActionResult DeleteBatch(int id)
        {
            var token = Convert.ToString(TempData["UserToken"]);
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            HttpClientHelper.ExecuteDeleteApiMethod<Batch>(baseUrl, "/Batch/DeleteBatch?", "Id=" + id, token);
            return RedirectToAction("ManageBatch", new { Area = "Shared" });
        }
    }
}
