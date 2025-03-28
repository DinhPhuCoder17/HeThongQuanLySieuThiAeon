﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTO;
using Microsoft.Identity.Client;

namespace DAL
{
    public class DataProvider
    {
        private String connectionStr = @"Data Source=DESKTOP-9UCJD9K;Initial Catalog=QuanLySieuThiAEON;Integrated Security=True;TrustServerCertificate=True";

        //Liêm
        //private String connectionStr = @"Data Source=LAPTOP-CKE458TU;Initial Catalog=QuanLySieuThiAEON;Integrated Security=True;Encrypt=True";

        //private String connectionStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySieuThiAEON;Integrated Security=True";

        //Phú laptop
        //private String connectionStr = @"Data Source=LAPTOP-P1IHVTIA;Initial Catalog=QuanLySieuThiAEON;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        //Phú pc
        private String connectionStr = @"Data Source=ADMIN\MSSQLSERVER01;Initial Catalog=QuanLySieuThiAEON;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        //Tạo singleton
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider(); // Khởi tạo nếu chưa có
                }
                return instance; // Trả về instance hiện tại
            }
        }


        private DataProvider() { }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if(parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara) { 
                        if(item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = null;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
            }
            return data;
        }

        //Hàm thực thi query có 1 tham số giống nhau nhưng có hai biến trong SQL
        public DataTable ExecuteQueryOneParameter(string query, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                            break;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }


    }
}
