using iTrainee.Models;
using iTrainee.Services.Implementations;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iTrainee.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorService service = new MentorService();
        // GET: api/<MentorController>
        [HttpGet]
        public IEnumerable<Topics> Get()
        {
            IEnumerable<Topics> result = service.GetAllTopics();
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
