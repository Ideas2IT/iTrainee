using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Data.Interfaces
{
	public interface IUserTopicsRepository
	{
		IEnumerable<UserTopics> GetAllUserTopics(int batchId);

		IEnumerable<Topics> GetUserTopicsByUserId(int id);

		IEnumerable<SubTopics> GetSubTopicsByUserIdAndTopicId(int userId, int topicId);
        DailyProgress GetSubTopicOfUser(int userId, int subTopicId);
		bool UpdateDailyProgress(DailyProgress dailyProgress);

		bool InsertUserTopic(UserTopics userTopic);
	}
}
