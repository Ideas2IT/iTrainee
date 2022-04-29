using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace iTrainee.Data.DataManager
{
    public interface IDataManager
    {
        int ExecuteReader(string query, List<SqlParameter> parameters);
        int ExecuteNonQuery(string storeProcedureName, List<SqlParameter> parameters, string outputParam);
        int ExecuteScalar(string query);
        DataSet ExecuteStoredProcedure(string storeProcedureName, List<SqlParameter> parameters);
        DataSet ExecuteStoredProcedure(string storeProcedureName);
        DataSet ExecuteQuery(string query, List<SqlParameter> parameters);
    }
}
