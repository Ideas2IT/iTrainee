﻿using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class StreamRepository : IStreamRepository
    {
        IDataManager _dataManager = null;

        public StreamRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool DeleteStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Stream GetStream(int id)
        {
            var stream = new Stream();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetStream", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        stream.Id = Convert.ToInt32(item["id"]);
                        stream.Name = Convert.ToString(item["Name"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stream;
        }

        public IEnumerable<Stream> GetStreams()
        {
            var streams = new List<Stream>();
            try
            {
                var parameters = new List<SqlParameter>();
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetStreams", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        streams.Add(new Stream
                        {
                            Id = Convert.ToInt32(item["id"]),
                            Name = Convert.ToString(item["Name"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return streams;
        }

        public bool InsertStream(Stream stream)
        {
            var isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Name",
                    Value = stream.Name
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedBy",
                    Value = stream.InsertedBy
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "InsertedOn",
                    Value = stream.InsertedOn
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedBy",
                    Value = stream.UpdatedBy
                });

                parameters.Add(new SqlParameter
                {
                    ParameterName = "UpdatedOn",
                    Value = stream.UpdatedOn
                });


                DataSet result = _dataManager.ExecuteStoredProcedure("spInsertStream", parameters);
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

        public bool UpdateStream(Stream stream)
        {
            throw new NotImplementedException();
        }



    }
}

