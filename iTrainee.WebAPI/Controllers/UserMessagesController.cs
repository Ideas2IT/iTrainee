using iTrainee.Models;
using Microsoft.AspNetCore.Mvc;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserMessagesController : Controller
    {
        IUserMessagesService _userMessagesService = null;
        public UserMessagesController(IUserMessagesService userMessagesService)
        {
            _userMessagesService = userMessagesService;
        }

        [HttpGet]
        public IEnumerable<User> GetTrainees()
        {
            return _userMessagesService.GetTrainees();
        }

        [HttpPost]
        public bool AddUserMessage(Messages message)
        {
            return _userMessagesService.AddUserMessage(message);
        }

        [HttpGet]
        public string[] GetSelectedTrainees(int Id)
        {
            return _userMessagesService.GetSelectedTrainees(Id);
        }

        [HttpGet]
        public IEnumerable<UserMessages> GetTraineeMessagesByUserId(int Id)
        {
            return _userMessagesService.GetTraineeMessagesByUserId(Id);
        }
    }
}
