using Microsoft.AspNetCore.Mvc;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;
using iTrainee.Models;
using Microsoft.AspNetCore.Authorization;
using iTrainee.APIs.Interfaces;

namespace iTrainee.APIs.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService = null;
        IJWTManagerRepository _jWTManager = null;

        public UserController(IUserService userService, IJWTManagerRepository jWTManager)
        {
            _userService = userService;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<User> GetUsers(string role)
        {
            return _userService.GetUsers(role);
        }

        [HttpGet]
        [AllowAnonymous]
        public User GetUser(int id)
        {
            return _userService.GetUser(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public User GetUserByUserName(string userName, string password)
        {
            var user = _userService.GetUserByUserName(userName, password);
            User sendUserName = new User();
            sendUserName.UserName = userName;
            Tokens token = _jWTManager.Authenticate(sendUserName);
            string userToken = token.Token;
            user.Token = userToken;
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
