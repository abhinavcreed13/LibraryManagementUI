using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SQLDALManager : IDAL
    {
        SqlConnection connection;

        public SQLDALManager(string dbConnKey)
        {
            String connString = ConfigurationManager.ConnectionStrings[dbConnKey].ConnectionString;
            connection = new SqlConnection(connString);
        }

        public DataTable ExecuteQuery(string sqlQuery)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable ExecuteStoredProcedure(string procedureName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = command;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable ExecuteStoredProcedure<T>(string procedureName, List<T> parameters) where T : DbParameter
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            foreach (DbParameter param in parameters)
            {
                command.Parameters.Add(param);
            }
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
