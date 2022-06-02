using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class UserTopicsController : Controller
	{
		IUserTopicsService _userTopicsService = null;
		public UserTopicsController(IUserTopicsService subTopicsService)
		{
			_userTopicsService = subTopicsService;
		}

		[HttpGet]
		public IEnumerable<UserTopics> GetAllUserTopics(int batchId)
		{
			List<UserTopics> users = new List<UserTopics>();
			try
            {
				users = (List<UserTopics>)_userTopicsService.GetAllUserTopics(batchId);
			} catch (Exception)
            {
				users = new List<UserTopics>();
            }
			
			return users;
		}

		[HttpPost]
		public bool AddUserTopic(UserTopics userTopic)
		{
			return _userTopicsService.AddUserTopic(userTopic);
		}
		

		public IEnumerable<SubTopics> GetSubTopicsByUserIdAndTopicId(int userId, int topicId)
        {
			return _userTopicsService.GetSubTopicsByUserIdAndTopicId(userId, topicId);
		}

		public IEnumerable<Topics> GetUserTopicsByUserId(int id)
		{
			return _userTopicsService.GetUserTopicsByUserId(id);
		}

		public DailyProgress GetSubTopicOfUser(int userId, int subTopicId)
        {
			return _userTopicsService.GetSubTopicOfUser(userId, subTopicId);
		}

		public bool UpdateDailyProgress(DailyProgress dailyProgress)
		{
			return _userTopicsService.UpdateDailyProgress(dailyProgress);
		}
	}
}
