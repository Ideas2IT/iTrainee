using iTrainee.Data.DataManager;
using System;
using System.Collections.Generic;
using System.Text;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class MessagesRepository : IMessagesRepository
    {
        IDataManager _dataManager = null;

        public MessagesRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<Messages> GetMessagesByUserId(int Id)
        {
            List<Messages> messages = new List<Messages>();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "FromId",
                    Value = Id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetMessagesByUserId", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        messages.Add(new Messages
                        {
                            Id = Convert.ToInt32(item["Id"]),
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

        public IEnumerable<UserMessages> GetUserMessagesByMessageId(int Id)
        {
            List<UserMessages> userMessages = new List<UserMessages>();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = Id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("[spGetUserMessages]", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        userMessages.Add(new UserMessages
                        {
                            Sender = Convert.ToString(item["Sender"]),
                            Receiver = Convert.ToString(item["Receiver"]),
                            Message = Convert.ToString(item["Message"]),
                            Comments = Convert.ToString(item["Comments"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userMessages;
        }

        public bool DeleteMessage(int id)
        {
            bool isDeleted = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                _dataManager.ExecuteStoredProcedure("spDeleteMessages", parameters);
                isDeleted = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }
    }
}
