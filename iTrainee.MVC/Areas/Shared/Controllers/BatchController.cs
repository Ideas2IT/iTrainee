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

        public BatchController(ILogger<BatchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult ManageBatch()
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            var batchList = HttpClientHelper.ExecuteGetAllApiMethod<Batch>(baseUrl, "/Batch/GetAllBatches");

            return View(batchList);
        }

        [HttpGet]
        public IActionResult AddEditBatch(int id)
        {
            Batch batch = new Batch();
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            batch = (Batch)HttpClientHelper.ExecuteGetApiMethod<Batch>(baseUrl, "/Batch/Get?", "Id=" + id);
            batch.MentorList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Mentor");
            batch.TraineeList = (List<User>)HttpClientHelper.ExecuteGetAllApiMethod<User>(baseUrl, "/User/GetUsers?role=Trainee");
            batch.StreamList = (List<Stream>)HttpClientHelper.ExecuteGetAllApiMethod<Stream>(baseUrl, "/Stream/GetAllstreams");

            if (id > 0)
            {
                batch.SelectedMentorIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedMentors?Id=" + id);
                batch.SelectedTraineeIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchUser/GetSelectedTrainees?Id=" + id);
                batch.SelectedStreamIds = HttpClientHelper.ExecuteGetIdsApiMethod<string[]>(baseUrl, "/BatchStream/GetSelectedStreams?Id=" + id);
            }
           
            return View(batch);
        }

        [HttpPost]
        public IActionResult AddEditBatch(Batch batch)
        {
            var baseUrl = _configuration.GetValue(typeof(string), "ApiURL").ToString();
            int batchId = HttpClientHelper.ExecuteInsertBatchPostApiMethod<Batch>(baseUrl, "/Batch/AddBatch", batch);
            batch.Id = batchId;
            string[] UserIdsArray = batch.SelectedMentorIds.Concat(batch.SelectedTraineeIds).ToArray();
            batch.StringUserIds = string.Join(",", UserIdsArray);
            batch.StringStreamIds = string.Join(",", batch.SelectedStreamIds);
            HttpClientHelper.ExecutePostApiMethod<Batch>(baseUrl, "/BatchUser/AddBatchUser", batch);
            return RedirectToAction("ManageBatch", new { Area="Shared" });
        }
    }
}
