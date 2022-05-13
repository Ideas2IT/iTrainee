﻿using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Data.Interfaces
{
    public interface IBatchUserRepository
    {
        bool InsertBatchUser(Batch batch);

        string[] GetSelectedMentors(int id);

        string[] GetSelectedTrainees(int id);

        bool UpdateBatchUser(Batch batch);

        bool UnassignUserId(Batch batch);

    }
}
