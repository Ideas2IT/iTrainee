using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IBatchRepository
    {
        Batch GetBatch(int id);
        IEnumerable<Batch> GetAllBatches();
        bool InsertBatch(Batch batch);
        bool UpdateBatch(Batch batch);
        bool DeleteBatch(int id);

    }
}
