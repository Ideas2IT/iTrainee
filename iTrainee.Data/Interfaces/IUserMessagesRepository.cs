using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IUserMessagesRepository
    {
        IEnumerable<User> GetTrainees();
        bool AddUserMessage(Messages message);
    }
}
