using System;
using System.Collections.Generic;
using iTrainee.Models;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IMessagesRepository
    {
        IEnumerable<UserMessages> GetMessagesByUserId(int Id, string role);
        IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id);
        bool DeleteMessage(int Id);
        int AddMessage(Messages message);
        Messages GetMessageById(int Id);
    }
}
