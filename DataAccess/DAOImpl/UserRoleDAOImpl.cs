using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOImpl
{
    public class UserRoleDAOImpl : IUserRoleDAO
    {
        public List<UserRoleDTO> GetUserRoleByUserID(int UserID)
        {
            var result = new List<UserRoleDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetUserRoleByUserID", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_UserID", UserID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new UserRoleDTO
                    {
                        UserRoleID = int.Parse(read["UserRoleID"].ToString()),
                        RoleID = int.Parse(read["RoleID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString())
                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UserGetDefaultRole(int UserID)
        {
            var result = 0;

            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_UserGetCustomerRole", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

        public List<UserRoleDTO> UserRoles_GetList()
        {
            var result = new List<UserRoleDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListUserRole", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new UserRoleDTO
                    {
                        UserRoleID = int.Parse(read["UserRoleID"].ToString()),
                        RoleID = int.Parse(read["RoleID"].ToString()),
                        UserID = int.Parse(read["UserID"].ToString())                 
                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
