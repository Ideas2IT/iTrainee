using iTrainee.Data;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
	public class UserTopicsService : IUserTopicsService
	{
		private IUserTopicsRepository _userTopicsRepository;

		public UserTopicsService(IUserTopicsRepository userTopicsRepository)
		{
			_userTopicsRepository = userTopicsRepository;
		}

		public IEnumerable<UserTopics> GetAllUserTopics(int batchId)
		{
			List<UserTopics> users = new List<UserTopics>();
			
				users = (List<UserTopics>)_userTopicsRepository.GetAllUserTopics(batchId);
			return users;
		}
	
		public bool AddUserTopic(UserTopics userTopic)
		{
			return _userTopicsRepository.InsertUserTopic(userTopic);
		}

		public IEnumerable<SubTopics> GetSubTopicsByUserIdAndTopicId(int userId, int topicId)
        {
			return _userTopicsRepository.GetSubTopicsByUserIdAndTopicId(userId, topicId);

		}

		public DailyProgress GetSubTopicOfUser(int userId, int subTopicId)
        {
			return _userTopicsRepository.GetSubTopicOfUser(userId, subTopicId);
		}

		public IEnumerable<Topics> GetUserTopicsByUserId(int id)
		{
			return _userTopicsRepository.GetUserTopicsByUserId(id);
		}

		public bool UpdateDailyProgress(DailyProgress dailyProgress)
		{
			return _userTopicsRepository.UpdateDailyProgress(dailyProgress);
		}
	}
}
