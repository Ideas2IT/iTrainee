﻿using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace iTrainee.Data
{
    public class UserTopicsRepository : IUserTopicsRepository
    {
        IDataManager _dataManager = null;

        public UserTopicsRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<UserTopics> GetAllUserTopics()
        {
            User user = new User();
            var userTopicsList = new List<UserTopics>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserTopics");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        userTopicsList.Add(new UserTopics
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            Username = Convert.ToString(item["UserName"]),
                            TopicName = Convert.ToString(item["Name"]),
                            SubTopicName = Convert.ToString(item["SubTopicName"])

                            //TopicName = (string[])item["TopicsName"],
                            //SubTopicName = (string[])item["SubTopicName"]
                        }); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userTopicsList;
        }

        public IEnumerable<Topics> GetUserTopicsByUserId(int id)
        {
            List<Topics> userTopics = new List<Topics>();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = id
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetUserTopicsByUserId", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        userTopics.Add(new Topics
                        {
                            Id = Convert.ToInt32(item["TopicId"]),
                            Name = Convert.ToString(item["TopicName"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userTopics;
        }

        public IEnumerable<SubTopics> GetSubTopicsByUserIdAndTopicId(int userid, int topicId)
        {
            List<SubTopics> userSubTopics = new List<SubTopics>();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = userid
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "TopicId",
                    Value = topicId
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSubTopicsByUserId", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        userSubTopics.Add(new SubTopics
                        {
                            Id = Convert.ToInt32(item["SubTopicId"]),
                            UserId = Convert.ToInt32(item["UserId"]),
                            TopicId = Convert.ToInt32(item["TopicId"]),
                            Name = Convert.ToString(item["SubTopicName"]),
                            Percentage = Convert.ToInt32(item["Percentage"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userSubTopics;
        }

        public DailyProgress GetSubTopicOfUser(int userId, int subTopicId)
        {
            DailyProgress dailyProgress = new DailyProgress();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = userId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "SubTopicId",
                    Value = subTopicId
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSubTopicOfUser", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        dailyProgress.UserId = Convert.ToInt32(item["UserId"]);
                        dailyProgress.SubTopicId = Convert.ToInt32(item["SubTopicId"]);
                        dailyProgress.StartDate = Convert.ToDateTime(item["StartDate"]);
                        dailyProgress.EndDate = Convert.ToDateTime(item["EndDate"]);
                        dailyProgress.MentorComments = Convert.ToString(item["MentorComments"]);
                        dailyProgress.TraineeComments = Convert.ToString(item["TraineeComments"]);
                        dailyProgress.Percentage = Convert.ToInt32(item["Percentage"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dailyProgress;
        }

        public bool UpdateDailyProgress(DailyProgress dailyProgress)
		{
            bool isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UserId",
                    Value = dailyProgress.UserId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "SubTopicId",
                    Value = dailyProgress.SubTopicId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Status",
                    Value = dailyProgress.Status
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "TraineeComments",
                    Value = dailyProgress.TraineeComments
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Percentage",
                    Value = dailyProgress.Percentage
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spUpdateDailyProgress", parameters);
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
