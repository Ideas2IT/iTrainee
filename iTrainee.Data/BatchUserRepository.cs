using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace iTrainee.Data
{
    public class BatchUserRepository : IBatchUserRepository
    {
        IDataManager _dataManager = null;
        public BatchUserRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public bool InsertBatchUser(Batch batch)
        {
            DataSet result = null;
            var isSuccess = false;

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "BatchId",
                    Value = batch.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = batch.StringUserIds
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

                result = _dataManager.ExecuteStoredProcedure("spInsertBatchUser", parameters);

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

        public string[] GetSelectedMentors(int id)
        {
            string[] selectedMentorIds = null;
            List<string> idList = new List<string>();

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "BatchId",
                    Value = id
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSelectedMentors", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        idList.Add(Convert.ToString(item["UserId"]));
                    }
                }
                selectedMentorIds = idList.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return selectedMentorIds;
        }

        public string[] GetSelectedTrainees(int id)
        {
            string[] selectedTraineeIds = null;
            List<string> idList = new List<string>();

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "BatchId",
                    Value = id
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSelectedTrainees", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        idList.Add(Convert.ToString(item["UserId"]));
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

        public bool UpdateBatchUser(Batch batch)
        {
            DataSet result = null;
            var isSuccess = false;

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "BatchId",
                    Value = batch.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = batch.StringUserIds
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

                result = _dataManager.ExecuteStoredProcedure("spUpdateBatchUser", parameters);

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

        public bool UnassignUserId(Batch batch)
        {
            DataSet result = null;
            var isSuccess = false;

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "BatchId",
                    Value = batch.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = batch.StringUserIds
                });

                result = _dataManager.ExecuteStoredProcedure("spRemoveUserIdInBatchUser", parameters);

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

        public IEnumerable<User> GetUnassignedTrainees()
        {
            var unassignedTrainees = new List<User>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUnassignedTrainees");
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
