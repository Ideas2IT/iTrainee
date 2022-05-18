using iTrainee.Data;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Implementations
{
    public class TopicsService : ITopicsService
    {
        private ITopicsRepository _topicsRepository;

        public TopicsService()
        {
        }

        public TopicsService(ITopicsRepository topicsRepository)
        {
            _topicsRepository = topicsRepository;
        }

        public bool DeleteTopic(int id)
        {
            return _topicsRepository.DeleteTopic(id);

        }

        public Topics GetTopic(int id)
        {
            return _topicsRepository.GetTopic(id);
        }

        public IEnumerable<Topics> GetAllTopics()
        {
            return _topicsRepository.GetAllTopics();
        }

        public bool InsertTopic(Topics topic)
        {
            return _topicsRepository.InsertTopic(topic);
        }

        public bool UpdateTopic(Topics topic)
        {
            return _topicsRepository.UpdateTopic(topic);
        }
    }
}
