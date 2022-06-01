using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
    public class MessagesService : IMessagesService
    {
        IMessagesRepository _messagesRepository = null;

        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }
        public IEnumerable<Messages> GetMessagesByUserId(int Id)
        {
            return _messagesRepository.GetMessagesByUserId(Id);
        }

        public IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id)
        {
            return _messagesRepository.GetUserMessagesByMessageId(Id);
        }

        public bool DeleteMessage(int Id)
        {
            return _messagesRepository.DeleteMessage(Id);
        }

        public int AddMessage(Messages message)
        {
            return _messagesRepository.AddMessage(message);
        }

        public Messages GetMessageById(int Id)
        {
            return _messagesRepository.GetMessageById(Id);
        }
    }
}
