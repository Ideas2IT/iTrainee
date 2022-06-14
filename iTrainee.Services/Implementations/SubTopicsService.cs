using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;
using iTrainee.Data.Interfaces;

namespace iTrainee.Services.Implementations
{
    public class SubTopicsService : ISubTopicsService
    {
        ISubTopicsRepository _subTopicsRepository = null;
        ITopicsService _topicsService = null;

        public SubTopicsService(ISubTopicsRepository subTopicsRepository)
        {
            _subTopicsRepository = subTopicsRepository;
        }

        public bool AddSubTopic(SubTopics topic)
        {
            return _subTopicsRepository.InsertSubTopics(topic);
        }

        public bool DeleteSubTopics(int id)
        {
           return _subTopicsRepository.DeleteSubTopics(id);
        }

        public IEnumerable<SubTopics> GetAllSubTopics()
        {
            return _subTopicsRepository.GetAllSubTopics();
        }

        public SubTopics GetSubTopic(int id)
        {
            return _subTopicsRepository.GetSubTopic(id);
        }

        public bool UpdateSubTopic(SubTopics topic)
        {
            return _subTopicsRepository.UpdateSubTopic(topic);
        }
    }
}
