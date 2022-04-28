using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Data
{
    public class UserRepository : IUserRepository
    {
        IDataManager _dataManager = null;

        public UserRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public User GetUser(int id)
        {
            User user = new User();
            return user;
        }
    }
}
