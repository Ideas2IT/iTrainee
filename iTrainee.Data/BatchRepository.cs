using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class BatchRepository : IBatchRepository
    {
        IDataManager _dataManager = null;

        public BatchRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool DeleteBatch(int id)
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
                _dataManager.ExecuteStoredProcedure("spDeleteBatch", parameters);
                isDeleted = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }

        public Batch GetBatch(int id)
        {
            var batch = new Batch();
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = id
                });
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetBatchById", parameters);
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        batch.Id = Convert.ToInt32(item["id"]);
                        batch.Name = Convert.ToString(item["Name"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return batch;
        }

        public IEnumerable<Batch> GetAllBatches()
        {
            var batches = new List<Batch>();
            try
            {
                DataSet result = _dataManager.ExecuteStoredProcedure("spGetBatch");
                if (result?.Tables?.Count != 0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        batches.Add(new Batch
                        {
                            Id = Convert.ToInt32(item["id"]),
                            Name = Convert.ToString(item["Name"]),
                            InsertedOn = Convert.ToDateTime(item["InsertedOn"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return batches;
        }

        public int InsertBatch(Batch batch)
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
                    ParameterName = "Name",
                    Value = batch.Name
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

                id = _dataManager.ExecuteReturnId("spInsertBatch", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;
        }

        public bool UpdateBatch(Batch batch)
        {
            var isSuccess = false;
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Id",
                    Value = batch.Id
                });
                parameters.Add(new SqlParameter
                {
                    ParameterName = "Name",
                    Value = batch.Name
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


                DataSet result = _dataManager.ExecuteStoredProcedure("spUpdateBatch", parameters);
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

