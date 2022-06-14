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

        public IEnumerable<UserMessages> GetMessagesByUserId(int Id, string Role)
        {
            List<UserMessages> messages = new List<UserMessages>();
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter
                    {
                        ParameterName = "UserId",
                        Value = Id
                    },
                    new SqlParameter
                    {
                        ParameterName = "Role",
                        Value = Role
                    }
                };
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserMessagesByRole", parameters);
                if (result?.Tables?.Count != 0)
                {
                    if (Role.Equals("Trainee"))
                    {
                        foreach (DataRow item in result.Tables[0].Rows)
                        {
                            messages.Add(new UserMessages
                            {
                                MessageId = Convert.ToInt32(item["MessageId"]),
                                Message = Convert.ToString(item["Message"]),
                                Sender = Convert.ToString(item["Sender"]),
                                IsRead = (bool)item["IsRead"]
                            });
                        }
                    }
                    else
                    {
                        foreach (DataRow item in result.Tables[0].Rows)
                        {
                            messages.Add(new UserMessages
                            {
                                MessageId = Convert.ToInt32(item["MessageId"]),
                                Message = Convert.ToString(item["Message"])
                            });
                        }
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
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter
                    {
                        ParameterName = "Id",
                        Value = Id
                    }
                };
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserMessages", parameters);
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

        public int AddMessage(Messages message)
        {
            int messageId = 0;
            try
            {
                var parameters = new List<SqlParameter>();
                if (message.Id > 0)
                {
                    parameters.Add(new SqlParameter
                    {
                        ParameterName = "Id",
                        Value = message.Id
                    });
                }
                else
                {
                    SqlParameter OutputParam = new SqlParameter("Id", SqlDbType.Int);
                    OutputParam.Direction = ParameterDirection.Output;
                    parameters.Add(OutputParam);
                }
                parameters.Add(new SqlParameter
                {
                    ParameterName = "FromId",
                    Value = message.FromId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Message",
                    Value = message.Message
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedBy",
                    Value = "Admin"
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedOn",
                    Value = DateTime.Now.Date
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedBy",
                    Value = "Admin"
                });

                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedOn",
                    Value = DateTime.Now.Date
                });

                messageId = _dataManager.ExecuteReturnId("spSaveMessage", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return messageId;
        }

        public Messages GetMessageById(int Id)
        {
            var message = new Messages();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = Id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetMessageById", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        message.Id = Convert.ToInt32(item["Id"]);
                        message.Message = Convert.ToString(item["Message"]);
                        message.FromId = Convert.ToInt32(item["FromId"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return message;
        }
    }
}
