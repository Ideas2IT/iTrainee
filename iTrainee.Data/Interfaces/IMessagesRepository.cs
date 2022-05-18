using System;
using System.Collections.Generic;
using iTrainee.Models;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IMessagesRepository
    {
        IEnumerable<Messages> GetMessagesByUserId(int Id);
        IEnumerable<UserMessages> GetUserMessagesByMessageId(int id);
        bool DeleteMessage(int id);
    }
}
