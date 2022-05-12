using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Services.Interfaces
{
    public interface IBatchStreamService
    {
        bool SaveBatchStream(Batch batch);

        string[] GetSelectedStreams(int id);

        bool UpdateBatchStream(Batch batch);

        bool UnassignStreamId(Batch batch);

    }
}
