using Microsoft.AspNetCore.Mvc;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;
using iTrainee.Models;
using Microsoft.AspNetCore.Authorization;

namespace iTrainee.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        IBatchService _batchService = null;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Batch> GetAllBatches()
        {
            return _batchService.GetAllBatches();
        }

        [HttpGet]
        [AllowAnonymous]
        public Batch Get(int id)
        {
            return _batchService.GetBatch(id);
        }

        [HttpPost]
        public int AddBatch(Batch batch)
        {
            return _batchService.InsertBatch(batch);
        }

        [HttpPost]
        public bool UpdateBatch(Batch batch)
        {
            return _batchService.UpdateBatch(batch);
        }

        [HttpDelete]
        public bool DeleteBatch(int id)
        {
            return _batchService.DeleteBatch(id);
        }
    }
}
