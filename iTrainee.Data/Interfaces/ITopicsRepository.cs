using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data
{
    public interface ITopicsRepository
    {
        List<Topics> GetAllTopics();
        Topics GetTopic(int id);
        bool InsertTopic(Topics topic);
        bool UpdateTopic(Topics topic);
        bool DeleteTopic(int id);
    }
}