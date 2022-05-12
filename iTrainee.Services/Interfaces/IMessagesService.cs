using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IMessagesService
    {
        IEnumerable<Messages> GetAllMessages();
    }
}
