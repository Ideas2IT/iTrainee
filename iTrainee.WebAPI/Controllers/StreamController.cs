using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
    [Produces("application/json")]
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
            var result = _streamService.GetStream(id);
            return result;
        }

        [HttpPost]
        public bool AddStream(Stream stream)
        {
            var result = _streamService.InsertStream(stream);
            return result;
        }

        [HttpPost]
        public bool UpdateStream(Stream stream)
        {
            var result = _streamService.UpdateStream(stream);
            return result;
        }

        [HttpPost]
        public bool DeleteStream(Stream stream)
        {
            var result = _streamService.DeleteStream(stream);
            return result;
        }

    }
}
