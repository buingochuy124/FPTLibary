﻿using DataAccess.DAO;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.DAOImpl
{
    public class BookDAOImpl : IBookDAO
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
                        BookISBN = read["ISBN"].ToString(),

                        BookName = read["BookName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        Pages = int.Parse(read["Pages"].ToString()),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        BookImageURL = read["BookURL"].ToString(),
                        // SellerID = int.Parse(read["SellerID"].ToString()),
                        Author = read["Author"].ToString(),
                        Description = read["BookDescription"].ToString()


                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookDTO> Books_GetListByPage(int? PageNumber, int? NumberPerPage)
        {
            var result = new List<BookDTO>();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetBookPagination", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_PageNumber", PageNumber);
                cmd.Parameters.AddWithValue("@_NumberPerPage", NumberPerPage);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result.Add(new BookDTO
                    {
                        BookID = int.Parse(read["BookID"].ToString()),
                        BookISBN = read["ISBN"].ToString(),

                        BookName = read["BookName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        Pages = int.Parse(read["Pages"].ToString()),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        BookImageURL = read["BookURL"].ToString(),
                       // SellerID = int.Parse(read["SellerID"].ToString()),
                        Author = read["Author"].ToString(),
                        Description = read["BookDescription"].ToString()

                    });
                }

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Book_Create(string BookISBN, string BookName, string Author, double Cost, int Pages, int CategoryID,string Description,string BookImage)
        {
            var result = 0;
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_CreateBook", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookName", BookName);
                cmd.Parameters.AddWithValue("@_Cost", Cost);
                cmd.Parameters.AddWithValue("@_Pages", Pages);
                cmd.Parameters.AddWithValue("@_CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@_Author", Author);
                cmd.Parameters.AddWithValue("@_BookDescription", Description);
                cmd.Parameters.AddWithValue("@_BookImage", BookImage);
                cmd.Parameters.AddWithValue("@_BookISBN", BookISBN);


                cmd.Parameters.Add("@_ResponseCode", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@_ResponseCode"].Value != null ? Convert.ToInt32(cmd.Parameters["@_ResponseCode"].Value) : 0;

                return result;


            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public BookDTO Book_GetDetail(int BookID)
        {
            var result = new BookDTO();
            try
            {
                var sqlconn = ConnectDB.GetSqlConnection();

                SqlCommand cmd = new SqlCommand("SP_GetBookDetail", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_BookID", BookID);


                var read = cmd.ExecuteReader();
                while (read.Read())
                {
                    result = new BookDTO
                    {
                        BookID = int.Parse(read["BookID"].ToString()),
                        BookISBN = read["ISBN"].ToString(),

                        BookName = read["BookName"].ToString(),
                        Cost = double.Parse(read["Cost"].ToString()),
                        Pages = int.Parse(read["Pages"].ToString()),
                        CategoryID = int.Parse(read["CategoryID"].ToString()),
                        BookImageURL = read["BookURL"].ToString(),
                        // SellerID = int.Parse(read["SellerID"].ToString()),
                        Author = read["Author"].ToString(),
                        Description = read["BookDescription"].ToString()

                    };
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
