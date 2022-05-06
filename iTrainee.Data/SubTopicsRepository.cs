using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class SubTopicsRepository : ISubTopicsRepository
    {
        IDataManager _dataManager = null;

        public SubTopicsRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<SubTopics> GetAllSubTopics()
        {
            var subTopicsList = new List<SubTopics>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSubTopics");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        subTopicsList.Add(new SubTopics
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            TopicId = Convert.ToInt32(item["TopicId"]),
                            Name = Convert.ToString(item["Name"]),
                            ReferenceURL = Convert.ToString(item["ReferenceURL"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return subTopicsList;
        }

        public bool DeleteSubTopics(int id)
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
                _dataManager.ExecuteStoredProcedure("spDeleteSubTopic", parameters);
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
