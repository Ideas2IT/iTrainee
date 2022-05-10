﻿using iTrainee.Services.Interfaces;
using iTrainee.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using iTrainee.Models;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class SubTopicsController : Controller
    {
        ISubTopicsService _subTopicsService = null;
        public SubTopicsController (ISubTopicsService subTopicsService)
        {
            _subTopicsService = subTopicsService;
        }
        // GET: SubTopicsController
        [HttpGet]
        public IEnumerable<SubTopics> GetAllSubtopics()
        {
            return _subTopicsService.GetAllSubTopics();
        }

        [HttpGet]
        public SubTopics Get(int id)
        {
            return _subTopicsService.GetSubTopic(id);
        }

        [HttpPost]
        public bool AddTopic(SubTopics topic)
        {
            return _subTopicsService.AddSubTopic(topic);
        }

        [HttpPost]
        public bool UpdateTopic(SubTopics topic)
        {
            return _subTopicsService.UpdateTopic(topic);
        }

        // POST: SubTopicsController/Delete/5
        [HttpDelete]
        public bool DeleteSubTopics(int id)
        {
            return _subTopicsService.DeleteSubTopics(id);
        }
    }
}
