using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOImpl
{
    public class UserDAOImpl : UserDAO
    {
        public int User_Login(string UserAccount, string UserPassword)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDb.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_UserLogin", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserAccount", UserAccount);
                cmd.Parameters.AddWithValue("@_UserPassword", UserPassword);


                cmd.Parameters.Add("@_ResponseCode", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseCode"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value) : 0;

                return result;


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
