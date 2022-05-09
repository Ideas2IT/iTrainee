using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IBatchUserService
    {
        bool SaveBatchUser(Batch batch);

        string[] GetSelectedMentors(int id);

        string[] GetSelectedTrainees(int id);
    }
}
