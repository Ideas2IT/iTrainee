using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public IEnumerable<User> GetMentors()
        {
            return _userRepository.GetMentors();
        }
    }
}
