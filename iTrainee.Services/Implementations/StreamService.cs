using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Data
{
    public class StreamService : IStreamService
    {
        IStreamRepository _streamRepository = null;

        public StreamService(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        public bool DeleteStream(int id)
        {
           return _streamRepository.DeleteStream(id);
        }

        public bool DeleteStream(Stream stream)
        {
            throw new System.NotImplementedException();
        }

        public Stream GetStream(int id)
        {
            return _streamRepository.GetStream(id);
        }

        public IEnumerable<Stream> GetStreams()
        {
            return _streamRepository.GetStreams();
        }

        public bool InsertStream(Stream stream)
        {
            return _streamRepository.InsertStream(stream);
        }

        public bool UpdateStream(Stream stream)
        {
            return _streamRepository.UpdateStream(stream);
        }
    }
}
