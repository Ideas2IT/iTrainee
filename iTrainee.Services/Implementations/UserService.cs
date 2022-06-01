﻿using iTrainee.Data.Interfaces;
using iTrainee.Models;
using iTrainee.Services.Interfaces;
using iTrainee.Services.Security;
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
            User user = _userRepository.GetUser(id);
            user.Password = EncryptAndDecrypt.ConvertToDecrypt(user.Password);
            return user;
        }

        public IEnumerable<User> GetUsers(string role)
        {
            return _userRepository.GetUsers(role);
        }

        public IEnumerable<User> GetAssignedTrainees(int batchId)
        {
            return _userRepository.GetAssignedTrainees(batchId);
        }

        public IEnumerable<User> GetAssignedMentors(int batchId)
        {
            return _userRepository.GetAssignedMentors(batchId);
        }


        public IEnumerable<User> GetUsersByBatch(string role, string id)
        {
            return _userRepository.GetUsersByBatch(role, id);
        }
       
        public User GetUserByUserName(string userName, string password)
        {
            User user = _userRepository.GetUserByUserName(userName);
            user.Password = EncryptAndDecrypt.ConvertToDecrypt(user.Password);
            if (password != user.Password)
            {
                user.Password = null;
            }
            return user;
        }

        public IEnumerable<User> GetMentors(string role)
        {
            return _userRepository.GetUsers(role);
        }

        public bool SaveUser(User user)
        {
            user.Password = EncryptAndDecrypt.ConvertToEncrypt(user.Password);
            return _userRepository.InsertUser(user);
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public bool UpdateUser(User user)
        {
            user.Password = EncryptAndDecrypt.ConvertToEncrypt(user.Password);
            return _userRepository.UpdateUser(user);
        }

        public string[] GetAssignedBatchIds(int userId)
        {
            return _userRepository.GetAssignedBatchIds(userId);
        }
    }
}
