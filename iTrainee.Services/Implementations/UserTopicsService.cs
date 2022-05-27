using iTrainee.Data;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
	public class UserTopicsService : IUserTopicsService
	{
		private IUserTopicsRepository userTopicsRepository;

		public UserTopicsService(IUserTopicsRepository _userTopicsRepository)
		{
			userTopicsRepository = _userTopicsRepository;
		}

		public IEnumerable<UserTopics> GetAllUserTopics()
		{
			return userTopicsRepository.GetAllUserTopics();
		}
	
		public bool AddUserTopic(UserTopics userTopic)
		{
			return userTopicsRepository.InsertUserTopic(userTopic);
		}

	}
}
