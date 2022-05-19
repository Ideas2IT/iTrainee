using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserTopicsService
    {
        IEnumerable<UserTopics> GetAllUserTopics();
    }
}
