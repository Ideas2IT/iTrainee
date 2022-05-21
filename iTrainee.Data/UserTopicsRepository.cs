using iTrainee.Data.DataManager;
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
    }
}
