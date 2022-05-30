using System;
using System.Collections.Generic;
using iTrainee.Models;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IMessagesRepository
    {
        IEnumerable<Messages> GetMessagesByUserId(int Id);
        IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id);
        bool DeleteMessage(int Id);
        int AddMessage(Messages message);
        Messages GetMessageById(int Id);
    }
}
