using Microsoft.AspNetCore.Mvc;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;
using iTrainee.Models;

namespace iTrainee.APIs.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService = null;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IEnumerable<User> GetMentors()
        {
            var result = _userService.GetMentors();
            return result;
        }
    }
}
