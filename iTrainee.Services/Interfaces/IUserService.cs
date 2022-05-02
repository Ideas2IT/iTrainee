﻿using iTrainee.Models;
using System.Collections.Generic;

namespace iTrainee.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(int id);
        IEnumerable<User> GetMentors();
        bool SaveUser(User user);

    }
}
