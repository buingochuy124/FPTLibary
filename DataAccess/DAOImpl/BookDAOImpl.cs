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
    public class BookDAOImpl : BookDAO
    {
        public List<BookDTO> Books_GetList()
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListBook", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new BookDTO
                    {
                        BookID = int.Parse(read["BookID"].ToString()),
                        BookName = read["BookName"].ToString(),
                        Cost = int.Parse(read["Cost"].ToString()),
                        Quantity = int.Parse(read["Quantity"].ToString()),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
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
