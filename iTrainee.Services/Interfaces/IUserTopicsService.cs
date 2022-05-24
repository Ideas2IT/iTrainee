using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserTopicsService
    {
        IEnumerable<UserTopics> GetAllUserTopics();
        IEnumerable<Topics> GetUserTopicsByUserId(int id);

        IEnumerable<SubTopics> GetSubTopicsByUserId(int id);
        DailyProgress GetSubTopicOfUser(int userId, int subTopicId);
    }
}
