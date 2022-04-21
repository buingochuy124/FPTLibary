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
