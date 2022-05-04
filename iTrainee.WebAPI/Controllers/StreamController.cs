using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        IStreamService _streamService = null;
        public StreamController(IStreamService streamService)
        {
            _streamService = streamService;
        }
        [HttpGet]
        public IEnumerable<Stream> GetAllStreams()
        {
            var result = _streamService.GetStreams();
            return result;
        }

        [HttpGet]
        public Stream Get(int id)
        {
            return _streamService.GetStream(id);
        }

        [HttpPost]
        public bool AddStream(Stream stream)
        {
            return _streamService.InsertStream(stream);
        }

        [HttpPost]
        public bool UpdateStream(Stream stream)
        {
            return _streamService.UpdateStream(stream);
        }

        [HttpDelete]
        public bool DeleteStream(int id)
        {
            return _streamService.DeleteStream(id);
        }

    }
}
