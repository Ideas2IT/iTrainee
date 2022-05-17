using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
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
        public IEnumerable<Messages> GetMessagesByUserId(int Id)
        {
            return _messagesService.GetMessagesByUserId(Id);
        }

        [HttpGet]
        public IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id)
        {
            return _messagesService.GetUserMessagesByMessageId(Id);
        }

        [HttpPost]
        public bool DeleteMessage(int Id)
        {
            return _messagesService.DeleteMessage(Id);
        }
    }
}
