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
            topic.StreamId = 1;
            topic.ReferenceURL = "www.webAPI.com";
            topic.IsActive = true;
            topic.InsertedBy = "Admin";
            topic.InsertedOn = DateTime.Now;
            topic.UpdatedBy = "None";
            topic.UpdatedOn = DateTime.Now;

            topicsList.Add(topic);

            return topicsList;
        }
    }
}
