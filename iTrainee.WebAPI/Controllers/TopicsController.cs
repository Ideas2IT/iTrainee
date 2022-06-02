using iTrainee.Models;
using iTrainee.Services.Implementations;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iTrainee.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        ITopicsService _topicsService = null;

        public TopicsController(ITopicsService topicService)
        {
            _topicsService = topicService;
        }

        [HttpGet]
        public IEnumerable<Topics> GetAllTopics()
        {
            return _topicsService.GetAllTopics();
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Topics> GetTopicsByStreamId(int streamId)
        {
            return _topicsService.GetTopicsByStreamId(streamId);
        }

        [HttpGet]
        [AllowAnonymous]
        public Topics Get(int id)
        {
            return _topicsService.GetTopic(id);
        }

        [HttpPost]
        public bool AddTopic(Topics topic)
        {
            return _topicsService.InsertTopic(topic);
        }

        [HttpPost]
        public bool UpdateTopic(Topics topic)
        {
            return _topicsService.UpdateTopic(topic);
        }

        [HttpDelete]
        public bool DeleteTopic(int id)
        {
            return _topicsService.DeleteTopic(id);
        }
    }
}
