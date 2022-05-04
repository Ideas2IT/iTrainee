using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface ITopicsService
    {
        Topics GetTopic(int id);
        IEnumerable<Topics> GetAllTopics();
        bool InsertTopic(Topics topic);
        bool UpdateTopic(Topics topic);
        bool DeleteTopic(int id);
    }
}
