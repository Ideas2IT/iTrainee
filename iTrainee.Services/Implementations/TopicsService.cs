using iTrainee.Data;
using iTrainee.Data.Interfaces;
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
        private ISubTopicsRepository _subTopicsRepository;

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
            IEnumerable<Topics> topicList = _topicsRepository.GetAllTopics();
            IEnumerable<SubTopics> subTopicList =  _subTopicsRepository.GetAllSubTopics();
            foreach (Topics topic in topicList)
            {
                foreach (SubTopics subTopic in subTopicList)
                {
                    if (topic.Id == subTopic.TopicId)
                    {
                        topic.SubTopicsList.Add(subTopic);
                    }
                }
            }
            return topicList;
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
