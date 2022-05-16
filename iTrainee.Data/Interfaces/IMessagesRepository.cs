using System;
using System.Collections.Generic;
using iTrainee.Models;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IMessagesRepository
    {
        IEnumerable<Messages> GetAllMessages();
    }
}
