using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IMessagesService
    {
        IEnumerable<Messages> GetMessagesByUserId(int Id);
        IEnumerable<UserMessages> GetUserMessagesByMessageId(int id);
        bool DeleteMessage(int id);
        int AddMessage(Messages message);
    }
}
