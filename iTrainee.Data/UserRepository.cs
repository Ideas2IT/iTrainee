using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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
            User user = new User();
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
    }
}
