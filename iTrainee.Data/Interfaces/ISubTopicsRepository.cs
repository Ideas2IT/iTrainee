using System.Collections.Generic;
using iTrainee.Models;

namespace iTrainee.Data.Interfaces
{
    public interface ISubTopicsRepository
    {
        /// <summary>
        /// Its gets all subtopics 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubTopics> GetAllSubTopics();
        bool DeleteSubTopics(int id);
        bool InsertSubTopics(SubTopics topic);
        SubTopics GetSubTopic(int id);
        bool UpdateSubTopic(SubTopics topic);
    }
}
