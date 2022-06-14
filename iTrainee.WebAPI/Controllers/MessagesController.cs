using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace iTrainee.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : Controller
    {
        IMessagesService _messagesService = null;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet]
        public IEnumerable<UserMessages> GetMessagesByUserId(int Id, string Role)
        {
            return _messagesService.GetMessagesByUserId(Id, Role);
        }

        [HttpGet]
        public IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id)
        {
            return _messagesService.GetUserMessagesByMessageId(Id);
        }

        [HttpGet]
        public Messages GetMessageById(int Id)
        {
            return _messagesService.GetMessageById(Id);
        }

        [HttpPost]
        public int AddMessage(Messages message)
        {
            return _messagesService.AddMessage(message);
        }

        [HttpDelete]
        public bool DeleteMessage(int Id)
        {
            return _messagesService.DeleteMessage(Id);
        }
    }
}
