using iTrainee.Models;
using iTrainee.Services.Implementations;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iTrainee.APIs.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        ITopicsService _topicsService = null;

        public TopicsController(ITopicsService topicService)
        {
            _topicsService = topicService;
        }


        // GET: api/<MentorController>
        [HttpGet]
        public IEnumerable<Topics> GetAllTopics()
        {
            IEnumerable<Topics> result = _topicsService.GetAllTopics();

            return result;
        }

        // GET api/<MentorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MentorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MentorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MentorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
