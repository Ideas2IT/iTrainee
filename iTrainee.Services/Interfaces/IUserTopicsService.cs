using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserTopicsService
    {
        IEnumerable<UserTopics> GetAllUserTopics();

        IEnumerable<SubTopics> GetSubTopicsByUserIdAndTopicId(int userId, int topicId);
        DailyProgress GetSubTopicOfUser(int userId, int subTopicId);
		bool UpdateDailyProgress(DailyProgress dailyProgress);
	    IEnumerable<Topics> GetUserTopicsByUserId(int id);  
    }
}
