using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;

namespace DAL
{
    public class DAL_Account
    {
            //private static DAL_Account instance;

            //public static DAL_Account Instance
            //{
            //    get { 
            //        if (instance == null) 
            //        instance = new DAL_Account();  
            //        return instance; 
            //    }
            //    private set { instance = value;  }
            //}
          
            //private DAL_Account(){}

        public bool Login(string username, string password)
        {
            string querry = "Select * from Quanly where Username = @username and Password = @password ";
            object result = DataProvider.Instance.ExecuteScalar(querry, new object[] { username, password });
            return (int)result > 0;
        }

        public DTO_Account GetAccountByUsername(string username)
        {
            string query = "SELECT * FROM Quanly WHERE Username = @username";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username });

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new DTO_Account()
                {
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Manhanvien = row["Manhanvien"].ToString()
                };
            }

            return null; // Không tìm thấy tài khoản
        }
    }
}
