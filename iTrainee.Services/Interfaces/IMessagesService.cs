using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IMessagesService
    {
        IEnumerable<UserMessages> GetMessagesByUserId(int Id, string Role);
        IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id);
        bool DeleteMessage(int Id);
        int AddMessage(Messages message);
        Messages GetMessageById(int Id);
    }
}
