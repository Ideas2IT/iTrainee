﻿using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace iTrainee.Data
{
    public class BatchStreamRepository : IBatchStreamRepository
    {
        IDataManager _dataManager = null;
        public BatchStreamRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public bool InsertBatchStream(Batch batch)
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

                int[] intStreamIds = Array.ConvertAll(batch.StringStreamIds.Split(','), int.Parse);
                foreach (int StreamId in intStreamIds)
                {
                    SqlParameter StreamIdParam = new SqlParameter();
                    StreamIdParam.ParameterName = "StreamId";
                    StreamIdParam.Value = StreamId;
                    parameters.Add(StreamIdParam);
                    result = _dataManager.ExecuteStoredProcedure("spInsertBatchStream", parameters);
                    parameters.Remove(StreamIdParam);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSuccess;
        }

        public string[] GetSelectedStreams(int id)
        {
            string[] selectedStreamIds = null;
            List<string> idList = new List<string>();

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "BatchId",
                    Value = id
                });

                DataSet result = _dataManager.ExecuteStoredProcedure("spGetStreamByBatchId", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        idList.Add(Convert.ToString(item["StreamId"]));
                    }
                }
                selectedStreamIds = idList.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return selectedStreamIds;
        }

        public bool UpdateBatchStream(Batch batch)
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

                int[] intStreamIds = Array.ConvertAll(batch.StringStreamIds.Split(','), int.Parse);
                foreach (int StreamId in intStreamIds)
                {
                    SqlParameter StreamIdParam = new SqlParameter();
                    StreamIdParam.ParameterName = "StreamId";
                    StreamIdParam.Value = StreamId;
                    parameters.Add(StreamIdParam);
                    result = _dataManager.ExecuteStoredProcedure("spUpdateBatchStream", parameters);
                    parameters.Remove(StreamIdParam);
                }

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

        public bool UnassignStreamId(Batch batch)
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

                int[] intStreamIds = Array.ConvertAll(batch.StringStreamIds.Split(','), int.Parse);
                foreach (int StreamId in intStreamIds)
                {
                    SqlParameter StreamIdParam = new SqlParameter();
                    StreamIdParam.ParameterName = "StreamId";
                    StreamIdParam.Value = StreamId;
                    parameters.Add(StreamIdParam);
                    result = _dataManager.ExecuteStoredProcedure("spRemoveStreamIdInBatchStream", parameters);
                    parameters.Remove(StreamIdParam);
                }

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
