using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IBatchStreamRepository
    {
        bool InsertBatchStream(Batch batch);

        string[] GetSelectedStreams(int id);
    }
}
