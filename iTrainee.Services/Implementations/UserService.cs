﻿using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using System.Collections.Generic;

namespace iTrainee.Services.Implementations
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

        public User GetUserByUserName(string userName, string password)
        {
            return _userRepository.GetUserByUserName(userName, password);
        }

        public IEnumerable<User> GetMentors()
        {
            return _userRepository.GetMentors();
        }

        public bool SaveUser(User user)
        {
            return _userRepository.InsertUser(user);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}
