using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
    public class BatchService : IBatchService
    {
        IBatchRepository _batchRepository = null;

        public BatchService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public bool DeleteBatch(int id)
        {
            return _batchRepository.DeleteBatch(id);
        }

        public Batch GetBatch(int id)
        {
            return _batchRepository.GetBatch(id);
        }

        public IEnumerable<Batch> GetAllBatches(int userId)
        {
            return _batchRepository.GetAllBatches(userId);
        }

        public int InsertBatch(Batch batch)
        {
            return _batchRepository.InsertBatch(batch);
        }

        public bool UpdateBatch(Batch batch)
        {
            return _batchRepository.UpdateBatch(batch);
        }
    }
}
