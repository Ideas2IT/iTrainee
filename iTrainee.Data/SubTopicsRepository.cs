﻿using iTrainee.Data.DataManager;
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

        public bool InsertSubTopics(SubTopics topic)
        {
            var isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "TopicId",
                    Value = topic.TopicId
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
                    Value = DateTime.Now.ToShortDateString()
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedBy",
                    Value = "Mentor"
                });

                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedOn",
                    Value = DateTime.Now.ToShortDateString()
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spInsertSubTopics", parameters);
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

        public SubTopics GetSubTopic(int id)
        {
            var subTopic = new SubTopics();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetSubTopicsById", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        subTopic.Id = Convert.ToInt32(item["Id"]);
                        subTopic.TopicId = Convert.ToInt32(item["TopicId"]);
                        subTopic.Name = Convert.ToString(item["Name"]);
                        subTopic.ReferenceURL = Convert.ToString(item["ReferenceURL"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return subTopic;
        }

        public bool UpdateSubTopic(SubTopics topic)
        {
            var isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter
                    {
                        ParameterName = "Id",
                        Value = topic.Id
                    },
                    new SqlParameter
                    {
                        ParameterName = "TopicId",
                        Value = topic.TopicId
                    },
                    new SqlParameter
                    {
                        ParameterName = "Name",
                        Value = topic.Name
                    },
                    new SqlParameter
                    {
                        ParameterName = "ReferenceURL",
                        Value = topic.ReferenceURL
                    },
                    new SqlParameter
                    {
                        ParameterName = "InsertedBy",
                        Value = "Mentor"
                    },
                    new SqlParameter
                    {
                        ParameterName = "InsertedOn",
                        Value = DateTime.Now.ToShortDateString()
                    },
                    new SqlParameter
                    {
                        ParameterName = "UpdatedBy",
                        Value = "Mentor"
                    },

                    new SqlParameter
                    {
                        ParameterName = "UpdatedOn",
                        Value = DateTime.Now.ToShortDateString()
                    }
                };

                DataSet result = _dataManager.ExecuteStoredProcedure("spUpdateSubTopics", parameters);
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
