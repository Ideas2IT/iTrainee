using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IMentorService
    {
        IEnumerable<Topics> GetAllTopics();
    }
}
