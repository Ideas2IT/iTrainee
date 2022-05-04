using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class UserRepository : IUserRepository
    {
        IDataManager _dataManager = null;
        public UserRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public User GetUser(int id)
        {
            var user = new User();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserById", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        user.Id = Convert.ToInt32(item["id"]);
                        user.FirstName = Convert.ToString(item["FirstName"]);
                        user.LastName = Convert.ToString(item["LastName"]);
                        user.DOB = Convert.ToDateTime(item["DOB"]);
                        user.Qualification = Convert.ToString(item["Qualification"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            var user = new User();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserName",
                    Value = userName
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserByUsrName", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        user.Id = Convert.ToInt32(item["id"]);
                        user.FirstName = Convert.ToString(item["FirstName"]);
                        user.LastName = Convert.ToString(item["LastName"]);
                        user.RoleId = Convert.ToInt32(item["RoleId"]);
                        user.UserName = Convert.ToString(item["UserName"]);
                        user.Password = Convert.ToString(item["Password"]);                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public IEnumerable<User> GetMentors()
        {
            var users = new List<User>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUser");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            FirstName = Convert.ToString(item["FirstName"]),
                            LastName = Convert.ToString(item["LastName"]),
                            DOB = Convert.ToDateTime(item["DOB"]),
                            Qualification = Convert.ToString(item["Qualification"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return users;
        }

        public bool DeleteUser(int id)
        {

            bool isDeleted;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                _dataManager.ExecuteStoredProcedure("spDeleteUser", parameters);
                isDeleted = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }

        public bool InsertUser(User user)
        {
            var isSuccess = false;

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = user.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "FirstName",
                    Value = user.FirstName
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "LastName",
                    Value = user.LastName
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "DOB",
                    Value = user.DOB
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "RoleId",
                    Value = 2
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserName",
                    Value = user.UserName
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Password",
                    Value = user.Password
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Qualification",
                    Value = user.Qualification
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedBy",
                    Value = "Admin"
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedOn",
                    Value = DateTime.Now
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedBy",
                    Value = "Mentor"
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedOn",
                    Value = DateTime.Now
                });


                DataSet result = _dataManager.ExecuteStoredProcedure("spSaveUser", parameters);
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
    }
}
