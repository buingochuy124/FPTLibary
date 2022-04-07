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
