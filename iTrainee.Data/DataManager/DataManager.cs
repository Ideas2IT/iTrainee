using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace iTrainee.Data.DataManager
{
    public class DataManager : IDataManager
    {
        private string _connectionString = null;

        public DataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int ExecuteNonQuery(string storeProcedureName, List<SqlParameter> parameters, string outputParam)
        {
            int result = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    connection.Open();
                    var command = new SqlCommand(storeProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameters(command, parameters);
                    command.ExecuteNonQuery();
                    result = (int)command.Parameters[outputParam].Value;
                    command.Dispose();
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public int ExecuteReader(string query, List<SqlParameter> parameters)
        {
            var result = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(parameters[0].ParameterName, SqlDbType.VarChar).Value = parameters[0].Value;

                    DataTable table = new DataTable();
                    table.Load(command.ExecuteReader());
                    if (table.Rows.Count > 0)
                        result = Convert.ToInt32(table.Rows[0][0]);
                    command.Dispose();
                    connection.Close();
                    command.Dispose();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public int ExecuteScalar(string query)
        {
            var result = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    command.CommandType = CommandType.Text;
                    result = Convert.ToInt32(command.ExecuteScalar());
                    command.Dispose();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public DataSet ExecuteStoredProcedure(string storeProcedureName, List<SqlParameter> parameters)
        {
            var result = new DataSet();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    connection.Open();
                    var command = new SqlCommand(storeProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SetParameters(command, parameters);
                    var adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(result);
                    adapter.Dispose();
                    command.Dispose();

                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    throw ex;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return result;
            }
        }

        public DataSet ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            var result = new DataSet();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    var adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    // SetParameters(command, parameters);
                    adapter.Fill(result);
                    adapter.Dispose();
                    command.Dispose();
                    connection.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                return result;
            }


        }

        private void SetParameters(SqlCommand command, List<SqlParameter> parameters)
        {
            if (parameters.Any())
            {
                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }
            }
        }
    }
}
