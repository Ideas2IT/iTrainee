using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
    internal class MessagesService : IMessagesService
    {
        IMessagesRepository _messagesRepository = null;

        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }
        public IEnumerable<Messages> GetAllMessages()
        {
            return _messagesRepository.GetAllMessages();
        }
    }
}
