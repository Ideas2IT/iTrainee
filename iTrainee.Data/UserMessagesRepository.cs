using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class UserMessagesRepository : IUserMessagesRepository
    {
        IDataManager _dataManager = null;
        public UserMessagesRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool AddUserMessage(Messages message)
        {
            DataSet result = null;
            var isSuccess = false;

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "MessageId",
                    Value = message.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "ToId",
                    Value = message.TraineesIdsString
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
                    Value = "Mentor"
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedOn",
                    Value = DateTime.Now.Date
                });

                result = _dataManager.ExecuteStoredProcedure("spInsertUserMessages", parameters);

                if (result.Tables.Count != 0)
                {
                    isSuccess = Convert.ToBoolean(result?.Tables?[0]?.Rows?[0]?[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSuccess;
        }

        public string[] GetSelectedTrainees(int Id)
        {
            string[] selectedTraineeIds = null;
            List<string> idList = new List<string>();

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "MessageId",
                    Value = Id
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSelectedUsers", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        idList.Add(Convert.ToString(item["ToId"]));
                    }
                }
                selectedTraineeIds = idList.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return selectedTraineeIds;
        }

        public IEnumerable<User> GetTrainees()
        {
            var unassignedTrainees = new List<User>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetTrainees");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        unassignedTrainees.Add(new User
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            UserName = Convert.ToString(item["UserName"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unassignedTrainees;
        }
    }
}
