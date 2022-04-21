using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class UserDAOImpl : IUserDAO
    {
        public List<UserDTO> Users_GetList()
        {
            var result = new List<UserDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListUser", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new UserDTO
                    {
                        UserId = int.Parse(read["UserId"].ToString()),
                        UserAccount = read["UserAccount"].ToString(),
                        UserPassword = read["UserPassword"].ToString(),
                        UserFullName = read["UserFullName"].ToString(),
                        UserAdress = read["UserAdress"].ToString(),
                        UserPhoneNumber = read["UserPhoneNumber"].ToString(),
                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int User_GetUserIDByUserAccount(string UserAccount)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetUserIDByUserAccount", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserAccount", UserAccount);


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

        public int User_Login(string UserAccount, string UserPassword)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

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

        public int User_Register(string UserAccount, string UserPassword, string UserFullName, string UserAddress, string UserPhoneNumber)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_UserRegister", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_UserAccount", UserAccount);
                cmd.Parameters.AddWithValue("@_UserPassword", UserPassword);
                cmd.Parameters.AddWithValue("@_UserFullName", UserFullName);
                cmd.Parameters.AddWithValue("@_UserAddress", UserAddress);
                cmd.Parameters.AddWithValue("@_UserPhoneNumber", UserPhoneNumber);


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
