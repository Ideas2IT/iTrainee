using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class UserAuditRepository : IUserAuditRepository
    {
        IDataManager _dataManager = null;

        public UserAuditRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public UserAudit GetUserAudit(int id)
        {
            var userAudit = new UserAudit();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserAuditById", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        userAudit.Id = Convert.ToInt32(item["Id"]);
                        userAudit.UserId = Convert.ToInt32(item["UserId"]);
                        userAudit.Date = Convert.ToDateTime(item["Date"]);
                        userAudit.SignIn = Convert.ToDateTime(item["SignIn"]);
                        userAudit.SignOut = Convert.ToDateTime(item["SignOut"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userAudit;
        }

        public int InsertUserAudit(UserAudit userAudit)
        {
            int id = 0;
            try
            {
                var parameters = new List<SqlParameter>();
                SqlParameter OutputParam = new SqlParameter("Id", SqlDbType.Int);
                OutputParam.Direction = ParameterDirection.Output;
                parameters.Add(OutputParam);

                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = userAudit.UserId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Date",
                    Value = userAudit.Date.Date
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "SignIn",
                    Value = userAudit.SignIn.ToLongTimeString()
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "SignOut",
                    Value = userAudit.SignOut.ToLongTimeString()
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedBy",
                    Value = "Mentor"
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

                id = _dataManager.ExecuteReturnId("spInsertUserAudit", parameters);               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;

        }
    }
}
