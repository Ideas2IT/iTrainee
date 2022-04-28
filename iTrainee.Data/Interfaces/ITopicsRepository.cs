using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Data
{
    public interface ITopicsRepository
    {
        List<Topics> GetAllTopics();
    }
}