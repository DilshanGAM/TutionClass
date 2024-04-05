using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionClass.DBConnection
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class MSSQLConnectionManager
    {
        private string connectionString;

        public MSSQLConnectionManager()
        {
            this.connectionString = "Data Source=DESKTOP-MT33GR4;Initial Catalog=tution_db;Integrated Security=True";
        }

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                // Handle the exception or log it
                throw new Exception("Error connecting to the database: " + ex.Message);
            }
        }

        public void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void DisposeConnection(SqlConnection connection)
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }

}