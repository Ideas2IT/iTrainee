using iTrainee.Data.DataManager;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace iTrainee.Data
{
    public class TopicsRepository : ITopicsRepository
    {
        IDataManager _dataManager = null;

        public TopicsRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public List<Topics> GetAllTopics()
        {
            var topicsList = new List<Topics>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetTopics");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        topicsList.Add(new Topics
                        {
                            Id = Convert.ToInt32(item["Id"]),
                            Name = Convert.ToString(item["TopicName"]),
                            StreamName = Convert.ToString(item["StreamName"]),
                            ReferenceURL = Convert.ToString(item["ReferenceURL"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topicsList;
        }

        public bool DeleteTopic(int id)
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
                _dataManager.ExecuteStoredProcedure("spDeleteTopics", parameters);
                isDeleted = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }

        public Topics GetTopic(int id)
        {
            var topic = new Topics();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetTopicById", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        topic.Id = Convert.ToInt32(item["Id"]);
                        topic.StreamId = Convert.ToInt32(item["StreamId"]);
                        topic.Name = Convert.ToString(item["Name"]);
                        topic.ReferenceURL = Convert.ToString(item["ReferenceURL"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topic;
        }

        public bool InsertTopic(Topics topic)
        {
            var isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "StreamId",
                    Value = topic.StreamId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Name",
                    Value = topic.Name
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "ReferenceURL",
                    Value = topic.ReferenceURL
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

                DataSet result = _dataManager.ExecuteStoredProcedure("spInsertTopics", parameters);
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

        public bool UpdateTopic(Topics topic)
        {
            var isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = topic.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "StreamId",
                    Value = topic.StreamId
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Name",
                    Value = topic.Name
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "ReferenceURL",
                    Value = topic.ReferenceURL
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

                DataSet result = _dataManager.ExecuteStoredProcedure("spUpdateTopics", parameters);
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
