using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserTopicsController : Controller
	{
		IUserTopicsService _userTopicsService = null;
		public UserTopicsController(IUserTopicsService subTopicsService)
		{
			_userTopicsService = subTopicsService;
		}
		public IEnumerable<UserTopics> GetAllUserTopics()
		{
			return _userTopicsService.GetAllUserTopics();
		}
	}
}
