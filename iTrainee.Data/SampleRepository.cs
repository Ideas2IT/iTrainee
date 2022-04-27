using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data
{
    public class SampleRepository : ISampleRepository
    {
        IDataManager _dataManager = null;

        public SampleRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        //public string InsertCareFabricActionLogDetail(CareFabricActionLogDetail careFabricActionLogDetail)
        //{
        //    string strActionLogDetailID = string.Empty;

        //    try
        //    {

        //        var parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "FacilityCode",
        //            Value = careFabricActionLogDetail.FacilityCode
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "LogType",
        //            Value = careFabricActionLogDetail.LogType
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Module",
        //            Value = careFabricActionLogDetail.Module
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Action",
        //            Value = careFabricActionLogDetail.Action
        //        });

        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Message",
        //            Value = careFabricActionLogDetail.Message
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "Exception",
        //            Value = careFabricActionLogDetail.Exception
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "InsertedBy",
        //            Value = careFabricActionLogDetail.InsertedBy
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "CareFabricStagingId",
        //            Value = careFabricActionLogDetail.CareFabricStagingId
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "InputPayLoad",
        //            Value = careFabricActionLogDetail.InputPayLoad
        //        });
        //        parameters.Add(new SqlParameter
        //        {
        //            ParameterName = "OutputPayLoad",
        //            Value = careFabricActionLogDetail.OutputPayLoad
        //        });

        //        DataSet result = _dataManager.ExecuteStoredProcedure("InsertCareFabricActionLogDetail", parameters);
        //        if (result.Tables.Count != 0)
        //        {
        //            DataTable dtblActionLogDetailID = result.Tables[0];
        //            if (dtblActionLogDetailID.Rows.Count > 0)
        //            {
        //                string[] careFabricActionLogDetailID = (from DataRow row in dtblActionLogDetailID.Rows
        //                                                        select row["CAREFABRICACTIONLOGDETAILID"].ToString()).ToArray();

        //                strActionLogDetailID = careFabricActionLogDetailID[0];

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return strActionLogDetailID;
        //}
    }
}
