using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BatchUserController : ControllerBase
    {
        IBatchUserService _batchUserService = null;
        public BatchUserController(IBatchUserService batchUserService)
        {
            _batchUserService = batchUserService;
        }

        [HttpPost]
        public bool AddBatchUser(Batch batch)
        {
            return _batchUserService.SaveBatchUser(batch);
        }

        [HttpGet]
        public string[] GetSelectedMentors(int id)
        {
            return _batchUserService.GetSelectedMentors(id);
        }

        [HttpGet]
        public string[] GetSelectedTrainees(int id)
        {
            return _batchUserService.GetSelectedTrainees(id);
        }

        [HttpPost]
        public bool UpdateBatchUser(Batch batch)
        {
            return _batchUserService.UpdateBatchUser(batch);
        }

        [HttpPost]
        public bool UnassignUserId(Batch batch)
        {
            return _batchUserService.UnassignUserId(batch);
        }
    }
}
