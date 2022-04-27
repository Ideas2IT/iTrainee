using iTrainee.Data.DataManager;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace iTrainee.Data
{
	public class StreamRepository
	{
        IDataManager _dataManager = null;

        public StreamRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

		//public string InsertStream(Stream stream)
		//{
		//	string strActionLogDetailID = string.Empty;

		//	try
		//	{

		//		var parameters = new List<SqlParameter>();
		//		parameters.Add(new SqlParameter
		//		{
		//			ParameterName = "Name",
		//			Value = stream.Name
		//		});
		//		parameters.Add(new SqlParameter
		//		{
		//			ParameterName = "InsertedBy",
		//			Value = stream.InsertedBy
		//		});
		//		parameters.Add(new SqlParameter
		//		{
		//			ParameterName = "InsertedOn",
		//			Value = stream.InsertedOn
		//		});
		//		parameters.Add(new SqlParameter
		//		{
		//			ParameterName = "UpdatedBy",
		//			Value = stream.UpdatedBy
		//		});

		//		parameters.Add(new SqlParameter
		//		{
		//			ParameterName = "UpdatedOn",
		//			Value = stream.UpdatedOn
		//		});
				

		//		DataSet result = _dataManager.ExecuteStoredProcedure("spInsertStream", parameters);
		//		if (result.Tables.Count != 0)
		//		{
		//			DataTable dtblActionLogDetailID = result.Tables[0];
		//			if (dtblActionLogDetailID.Rows.Count > 0)
		//			{
		//				string[] careFabricActionLogDetailID = (from DataRow row in dtblActionLogDetailID.
		//														select row["Stream"].ToString()).ToArray();

		//				strActionLogDetailID = careFabricActionLogDetailID[0];

		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}

		//	return strActionLogDetailID;
		//}
	}
}
