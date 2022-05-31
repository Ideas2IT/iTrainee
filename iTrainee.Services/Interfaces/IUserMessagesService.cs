﻿using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IUserMessagesService
    {
        IEnumerable<User> GetTrainees();
        bool AddUserMessage(Messages message);
    }
}
