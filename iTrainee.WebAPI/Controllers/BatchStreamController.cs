using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BatchStreamController : ControllerBase
    {
        IBatchStreamService _batchStreamService = null;
        public BatchStreamController(IBatchStreamService batchStreamService)
        {
            _batchStreamService = batchStreamService;
        }

        [HttpPost]
        public bool AddBatchStream(Batch batch)
        {
            return _batchStreamService.SaveBatchStream(batch);
        }

        [HttpGet]
        public string[] GetSelectedStreams(int id)
        {
            return _batchStreamService.GetSelectedStreams(id);
        }
    }
}
