using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data.Interfaces
{
    public interface IBatchRepository
    {
        Batch GetBatch(int id);
        IEnumerable<Batch> GetAllBatches(int userId);
        int InsertBatch(Batch batch);
        bool UpdateBatch(Batch batch);
        bool DeleteBatch(int id);

    }
}
