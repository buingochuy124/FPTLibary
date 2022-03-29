using System;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ConnectDB
    {
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlconn = null;
            try
            {
                var connectName = "Server=.;Database=FPTLibrary;Trusted_Connection=True;";

                sqlconn = new SqlConnection(connectName);

                if (sqlconn.State == System.Data.ConnectionState.Open)
                {
                    sqlconn.Close();
                }

                sqlconn.Open();
            }
            catch (Exception)
            {
                throw;
            }

            return sqlconn;

        }
    }
}
