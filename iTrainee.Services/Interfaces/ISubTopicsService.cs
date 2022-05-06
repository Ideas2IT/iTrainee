using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface ISubTopicsService
    {
        /// <summary>
        /// Gets All Sub Topics
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubTopics> GetAllSubTopics();

        bool DeleteSubTopics(int id);
    }
}
