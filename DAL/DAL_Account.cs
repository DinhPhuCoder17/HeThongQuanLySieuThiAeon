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
            string query = "SELECT Role, Password FROM Quanly WHERE Username = @username ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });

            if (result.Rows.Count > 0)
            {
                string storedHashedPassword = result.Rows[0]["Password"].ToString();

                // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã lưu không
                if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
                {
                    return result.Rows[0]["Role"].ToString(); // Đăng nhập thành công
                }
            }

            return null; // Trả về null nếu không tìm thấy tài khoản
        }

        // Lấy mã nhân viên từ database
        public string GetMaNV(string username, string password)
        {
            string query = "SELECT Manhanvien, Password FROM Quanly WHERE Username = @username ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });

            if (result.Rows.Count > 0)
            {
                string storedHashedPassword = result.Rows[0]["Password"].ToString();

                // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã lưu không
                if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
                {
                    return result.Rows[0]["Manhanvien"].ToString(); // Đăng nhập thành công
                }
            }

            return null; // Trả về null nếu không tìm thấy tài khoản
        }

        // Lấy Xoá từ bảng nhân viên
        public string CheckXoa(string username, string password)
        {
            string query = "SELECT Nhanvien.Xoa, Password FROM Quanly Join Nhanvien ON Nhanvien.Manhanvien = Quanly.Manhanvien WHERE Username = @username ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });

            if (result.Rows.Count > 0)
            {
                string storedHashedPassword = result.Rows[0]["Password"].ToString();
                int xoa = Convert.ToInt32(result.Rows[0]["Xoa"]);

                // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã lưu không
                if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
                {
                    // Kiểm tra trạng thái Xoa, nếu là 0 thì tài khoản đã dừng hoạt động
                    if (xoa == 0)
                    {
                        return "Tài khoản đã dừng hoạt động"; // Tài khoản đã nghỉ
                    }
                }
            }

            return null; // Trả về null nếu không tìm thấy tài khoản
        }
    }
}
