using Microsoft.AspNetCore.Mvc;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;
using iTrainee.Models;
using System;

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

        [HttpGet]
        public User GetUser(int id)
        {
            var user = _userService.GetUser(id);

            return user;
        }

        [HttpGet]
        public User GetUserByUserName(string userName)
        {
            var user = _userService.GetUserByUserName(userName);

            return user;
        }

        [HttpPost]
        public bool SaveUser(User user)
        {
            var isSuccess = _userService.SaveUser(user);

            return isSuccess;
        }

        [HttpDelete]
        public bool DeleteUser(int id)
        {
            var isSuccess = _userService.DeleteUser(id);

            return isSuccess;
        }
    }
}
