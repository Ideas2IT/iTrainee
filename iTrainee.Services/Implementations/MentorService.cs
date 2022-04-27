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
            List<Topics> topicsList = new List<Topics>();
            Topics topic = new Topics();
            topic.Id = 1;
            topic.Name = "Web API";
            Topics topic1 = new Topics();
            topic1.Id = 1;
            topic1.Name = "Test";

            topicsList.Add(topic);
            topicsList.Add(topic1);

            return topicsList;
        }
    }
}
