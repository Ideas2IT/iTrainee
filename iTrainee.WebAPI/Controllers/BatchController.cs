﻿using Microsoft.AspNetCore.Mvc;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;
using iTrainee.Models;
using Microsoft.AspNetCore.Authorization;

namespace iTrainee.APIs.Controllers
{
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
        public IEnumerable<Batch> GetAllBatches()
        {
            return _batchService.GetAllBatches();
        }

        [HttpGet]
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
