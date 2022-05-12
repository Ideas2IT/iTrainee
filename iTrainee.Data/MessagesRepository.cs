using iTrainee.Data.DataManager;
using System;
using System.Collections.Generic;
using System.Text;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System.Data;

namespace iTrainee.Data
{
    internal class MessagesRepository : IMessagesRepository
    {
        IDataManager _dataManager = null;

        public MessagesRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<Messages> GetAllMessages()
        {
            var messages = new List<Messages>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserMessages");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        messages.Add(new Messages
                        {
                            Id = Convert.ToInt32(item["id"]),
                            Message = Convert.ToString(item["Message"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messages;
        }
    }
}
