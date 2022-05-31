using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
{
    public class UserMessagesService : IUserMessagesService
    {
        private IUserMessagesRepository _userMessagesRepository;

        public UserMessagesService(IUserMessagesRepository userMessagesRepository)
        {
            _userMessagesRepository = userMessagesRepository;
        }

        public bool AddUserMessage(Messages message)
        {
            return _userMessagesRepository.AddUserMessage(message);
        }

        public string[] GetSelectedTrainees(int Id)
        {
            return _userMessagesRepository.GetSelectedTrainees(Id);
        }

        public IEnumerable<User> GetTrainees()
        {
            return _userMessagesRepository.GetTrainees();
        }
    }
}
