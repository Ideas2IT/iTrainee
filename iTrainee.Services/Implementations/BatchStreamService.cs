using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Implementations
{
    public class BatchStreamService : IBatchStreamService
    {
        private IBatchStreamRepository _batchStreamRepository;
        public BatchStreamService(IBatchStreamRepository batchStreamRepository)
        {
            _batchStreamRepository = batchStreamRepository;
        }

        public bool SaveBatchStream(Batch batch)
        {
            return _batchStreamRepository.InsertBatchStream(batch);
        }

        public string[] GetSelectedStreams(int id)
        {
            return _batchStreamRepository.GetSelectedStreams(id);
        }
    }
}
