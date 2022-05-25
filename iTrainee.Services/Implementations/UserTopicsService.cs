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

		public IEnumerable<UserTopics> GetAllUserTopics()
		{
			return _userTopicsRepository.GetAllUserTopics();
		}

		public IEnumerable<Topics> GetUserTopicsByUserId(int id)
        {
			return _userTopicsRepository.GetUserTopicsByUserId(id);

		}

		public IEnumerable<SubTopics> GetSubTopicsByUserId(int id)
        {
			return _userTopicsRepository.GetSubTopicsByUserId(id);
		}

		public DailyProgress GetSubTopicOfUser(int userId, int subTopicId)
        {
			return _userTopicsRepository.GetSubTopicOfUser(userId, subTopicId);
		}

		public bool UpdateDailyProgress(DailyProgress dailyProgress)
		{
			return _userTopicsRepository.UpdateDailyProgress(dailyProgress);
		}
	}
}
