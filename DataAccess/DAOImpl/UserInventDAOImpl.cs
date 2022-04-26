using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class UserInventDAOImpl : IUserInventDAO
    {

        public List<UserInventDTO> UserInvents_GetList()
        {
            var result = new List<UserInventDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListUserInvent", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new UserInventDTO
                    {
                        UserInventID = int.Parse(read["UserInventID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString()),
                        BookID = int.Parse(read["BookID"].ToString()),

                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public int UserInvent_AddBook(int BookID,int UserID)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_AddBookToInvent", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookID", BookID);
                cmd.Parameters.AddWithValue("@_UserID", UserID);
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

        public List<UserInventDTO> UserInvent_GetDetail(int UserID)
        {
            var result = new List<UserInventDTO>();


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetUserInvent", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserID", UserID);

                cmd.ExecuteNonQuery();



                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new UserInventDTO
                    {
                        UserInventID = int.Parse(read["UserInventID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString()),
                        BookID = int.Parse(read["BookID"].ToString()),

                    });
                }
                return result;


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
