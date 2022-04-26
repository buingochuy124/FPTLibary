using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class SellerDAOImpl : ISellerDAO
    {
        public List<SellerDTO> Sellers_GetList()
        {
            var result = new List<SellerDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetListSeller", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new SellerDTO
                    {

                        SellerID = int.Parse(read["SellerID"].ToString()),
                        SellerName = read["SellerName"].ToString(),
                    });
                }
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Seller_Create(string SellerName, DateTime SaleDate)
        {
            var result = 0;


            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateSeller", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_SaleDate", SaleDate);
                cmd.Parameters.AddWithValue("@_SellerName", SellerName);


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
