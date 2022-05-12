using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Implementations
{
    public class BatchUserService : IBatchUserService
    {
        private IBatchUserRepository _batchUserRepository;
        public BatchUserService(IBatchUserRepository batchUserRepository)
        {
            _batchUserRepository = batchUserRepository;
        }

        public bool SaveBatchUser(Batch batch)
        {
            return _batchUserRepository.InsertBatchUser(batch);
        }

        public string[] GetSelectedMentors(int id)
        {
            return _batchUserRepository.GetSelectedMentors(id);
        }

        public string[] GetSelectedTrainees(int id)
        {
            return _batchUserRepository.GetSelectedTrainees(id);
        }

        public bool UpdateBatchUser(Batch batch)
        {
            return _batchUserRepository.UpdateBatchUser(batch);
        }

        public bool UnassignUserId(Batch batch)
        {
            return _batchUserRepository.UnassignUserId(batch);
        }
    }
}
