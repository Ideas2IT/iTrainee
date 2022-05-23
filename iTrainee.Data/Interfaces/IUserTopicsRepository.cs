using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Data.Interfaces
{
	public interface IUserTopicsRepository
	{
		IEnumerable<UserTopics> GetAllUserTopics();

		IEnumerable<Topics> GetUserTopicsByUserId(int id);
	}
}
