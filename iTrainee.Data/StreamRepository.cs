using iTrainee.Data.DataManager;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace iTrainee.Data
{
	public class StreamRepository : IStreamRepository
	{
        IDataManager _dataManager = null;

        public StreamRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

		public void InsertStream(Stream stream)
		{
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


				_dataManager.ExecuteStoredProcedure("spInsertStream", parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
