using iTrainee.Data;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TopicsService(ITopicsRepository topicsRepository, ISubTopicsRepository subTopicsRepository)
        {
            _topicsRepository = topicsRepository;
            _subTopicsRepository = subTopicsRepository;
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
            List<SubTopics> subTopicList =  _subTopicsRepository.GetAllSubTopics().ToList();
            List<SubTopics> newList = null;
            foreach (Topics topic in topicList)
            {
                newList = new List<SubTopics>();
                foreach (SubTopics subTopic in subTopicList)
                {
                    if (topic.Id == subTopic.TopicId)
                    {
                        newList.Add(subTopic);
                    }
                }
                topic.SubTopics= newList;
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
