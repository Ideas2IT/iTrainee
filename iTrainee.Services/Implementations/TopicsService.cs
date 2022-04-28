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
        public IEnumerable<Topics> GetAllTopics()
        {
            List<Topics> topicsList = _topicsRepository.GetAllTopics();


            return topicsList;
        }
    }
}
