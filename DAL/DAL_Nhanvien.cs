using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.Threading;


namespace DAL
{
    public class DAL_Nhanvien
    {
        private static DAL_Nhanvien instance;
        public static DAL_Nhanvien Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_Nhanvien();
                return instance;
            }
        }

        private DAL_Nhanvien() { }

        //Hàm thêm nhân viên (dùng cho cả thêm quản lý)
        public bool InsertEmployee(string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string sdt, string rolenv, string username, string password, string role)
        {

            // Gọi stored procedure và lấy mã nhân viên mới
            string query = "EXEC themMaNhanvien @HoTen , @CCCD , @NgaySinh , @GioiTinh , @DiaChi , @Sodienthoai , @Vaitro ";
            switch (rolenv)
            {
                case "TCNS":
                    rolenv = "Tài Chính Nhân Sự";
                    break;
                case "Admin":
                    rolenv = "Quản Trị Hệ Thống";
                    break;
                case "Kho":
                    rolenv = "Quản Lý Kho";
                    break;
            }
            object[] parameters =
            {
            hoTen,
            cccd, // Mã hóa CCCD
            ngaySinh,
            gioiTinh,
            diaChi,     
            sdt, //Mã hoá SĐT
            rolenv
            };

            object result = DataProvider.Instance.ExecuteScalar(query, parameters);
            if (result == null) return false; // Thêm nhân viên thất bại

            string maNhanVienMoi = result.ToString(); // Lấy mã nhân viên vừa được tạo

            //Nếu nhân viên này là quản lý, thêm vào bảng Quanly
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Hash mật khẩu

                string queryQL = "INSERT INTO Quanly (Manhanvien, Username, Password, Role) " +
                                 "VALUES ( @Manhanvien , @Username , @Password , @Role )";

                object[] parametersQL = 
                { 
                    maNhanVienMoi, // Dùng mã nhân viên vừa lấy được
                    username,
                    hashedPassword,
                    role
                };
                return DataProvider.Instance.ExecuteNonQuery(queryQL, parametersQL) > 0;
            }
            return true;
        }

        //Lấy danh sách nhân viên
        public DataTable GetEmployeeList()
        {
            string query = @"
            SELECT 
            nv.Manhanvien,
            nv.Hoten, 
            LEFT(CCCD, 4) + '****' + RIGHT(CCCD, 4) as CCCD, 
            nv.Ngaysinh, 
            nv.Gioitinh, 
            nv.Diachi, 
            LEFT(CCCD, 3) + '****' + RIGHT(CCCD, 3) as Sodienthoai, 
            nv.Vaitro
            FROM Nhanvien nv
            WHERE nv.Xoa = 1";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Update Nhan vien
        public bool UpdateEmployee(string maNhanVien, string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai, string vaiTro)
        {
            string query = "UPDATE Nhanvien SET Hoten = @Hoten , CCCD = @CCCD , Ngaysinh = @Ngaysinh , Gioitinh = @Gioitinh , Diachi = @Diachi , Sodienthoai = @Sodienthoai , Vaitro = @Vaitro WHERE Manhanvien = @Manhanvien ";

            object[] parameters = { hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai, vaiTro, maNhanVien };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        //Delete Nhan vien
        public bool DeleteEmployee(string maNhanVien)
        {
            string query = "UPDATE Nhanvien SET Xoa = 0 WHERE Manhanvien = @Manhanvien ";
            object[] parameters = { maNhanVien };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool AddManager(string maNhanVien, string userName, string passWord, string role)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(passWord); // Hash mật khẩu

            string queryQL = "INSERT INTO Quanly (Manhanvien, Username, Password, Role) " +
                             "VALUES ( @Manhanvien , @Username , @Password , @Role )";

            object[] parametersQL =
            {
                    maNhanVien, // Dùng mã nhân viên vừa lấy được
                    userName,
                    hashedPassword,
                    role
                };
            return DataProvider.Instance.ExecuteNonQuery(queryQL, parametersQL) > 0;
        }

        // lấy Info nhân viên từ mã nhân viên
        public DataTable GetEmployeeInfo(string maNV)
        {
            string query = "SELECT * FROM Nhanvien WHERE Manhanvien = @Manhanvien";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maNV });
        }

        //Hàm UPDATE mật khẩu mới
        public bool UpdatePassword(string maNhanVien, string oldpassword, string password)
        {
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuery("Select * From Quanly where Manhanvien = @Manhanvien ", new object[] { maNhanVien});
                if (dt.Rows.Count > 0)
                {
                    string storedHashedPassword = dt.Rows[0]["Password"].ToString();

                    // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã lưu không
                    if (!BCrypt.Net.BCrypt.Verify(oldpassword, storedHashedPassword))
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Mật khẩu không đúng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                        {
                            MessageBox.Show("Password is not correct", "Nofication", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Hash mật khẩu
                string query = "UPDATE Quanly SET Password = @Password WHERE Manhanvien = @Manhanvien";

                object[] parameters = { hashedPassword, maNhanVien };

                return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
