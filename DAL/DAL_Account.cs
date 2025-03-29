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
        private static DAL_Account instance;

        public static DAL_Account Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_Account();
                return instance;
            }
        }

        private DAL_Account() { }

        // Lấy role từ database
        public string GetRole(string username, string password)
        {
            string query = "SELECT Role FROM Quanly WHERE Username = @username AND Password = @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["Role"].ToString();
            }

            return null; // Trả về null nếu không tìm thấy tài khoản
        }
    }
}
