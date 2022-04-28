using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IStreamRepository
    {
        Stream GetStream(int id);
        IEnumerable<Stream> GetStreams();
        bool InsertStream(Stream stream);
        bool UpdateStream(Stream stream);
        bool DeleteStream(Stream stream);

    }
}