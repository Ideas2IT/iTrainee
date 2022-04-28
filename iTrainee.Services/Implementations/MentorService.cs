using iTrainee.Data;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Implementations
{
    public class MentorService : IMentorService
    {
        public IEnumerable<Topics> GetAllTopics()
        {
            TopicsRepository topicRepo = new TopicsRepository();
            List<Topics> topicsList = topicRepo.GetAllTopics();


            return topicsList;
        }
    }
}
