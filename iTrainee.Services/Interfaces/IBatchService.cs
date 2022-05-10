using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IBatchService
    {
        Batch GetBatch(int id);
        IEnumerable<Batch> GetAllBatches();
        int InsertBatch(Batch batch);
        bool UpdateBatch(Batch batch);
        bool DeleteBatch(int id);
    }
}
