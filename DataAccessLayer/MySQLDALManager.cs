using MySql.Data.MySqlClient;
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
    public class MySQLDALManager : IDAL
    {
        MySqlConnection connection;

        public MySQLDALManager(string dbConnKey)
        {
            String connString = ConfigurationManager.ConnectionStrings[dbConnKey].ConnectionString;
            connection = new MySqlConnection(connString);
        }

        public DataTable ExecuteQuery(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteStoredProcedure(string procedureName)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = command;
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                //throw ex;
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable ExecuteStoredProcedure<T>(string procedureName, List<T> parameters) where T : DbParameter
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (DbParameter param in parameters)
            {
                command.Parameters.Add(param);
            }
            adapter.SelectCommand = command;
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
                //throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
