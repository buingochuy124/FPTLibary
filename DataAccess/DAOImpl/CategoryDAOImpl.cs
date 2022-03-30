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
    public class CategoryDAOImpl : CategoryDAO
    {
        public List<CategoryDTO> Categories_GetList()
        {
            var result = new List<CategoryDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListCategory", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new CategoryDTO
                    {
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        CategoryName = read["CategoryName"].ToString(),
                       
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
