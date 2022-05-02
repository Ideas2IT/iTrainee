using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IStreamService
    {
        Stream GetStream(int id);
        IEnumerable<Stream> GetStreams();
        bool InsertStream(Stream stream);
        bool UpdateStream(Stream stream);
        bool DeleteStream(Stream stream);
    }
}
