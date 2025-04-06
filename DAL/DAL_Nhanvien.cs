using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


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
            string query = "SELECT Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Vaitro FROM Nhanvien WHERE Xoa = 1";
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
        public bool UpdatePassword(string maNhanVien, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Hash mật khẩu
            string query = "UPDATE Quanly SET Password = @Password WHERE Manhanvien = @Manhanvien ";

            object[] parameters = { hashedPassword, maNhanVien };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        // Kiểm tra xem CCCD đã tồn tại chưa
        public bool IsCCCDExist(string cccd)
        {
            string query = "SELECT COUNT(*) FROM Nhanvien WHERE CCCD = @cccd ";
            object[] parameters = { cccd };
            object result = DataProvider.Instance.ExecuteScalar(query, parameters);
            int count = Convert.ToInt32(result); // Trả về true nếu CCCD tồn tại
            return count > 0;
        }

        // Kiểm tra xem SĐT đã tồn tại chưa
        public bool IsPhoneExist(string sdt)
        {
            string query = "SELECT COUNT(*) FROM Nhanvien WHERE Sodienthoai = @sdt ";
            object[] parameters = { sdt };
            object result = DataProvider.Instance.ExecuteScalar(query, parameters);
            int count = Convert.ToInt32(result); // Trả về true nếu SDT tồn tại
            return count > 0;
        }
        //Tìm kiếm nhân viên
        public DataTable timKiemNV(String tukhoa)
        {
            return DataProvider.Instance.ExecuteQueryOneParameter("Select Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai  From Nhanvien where (Manhanvien LIKE '%' + @tukhoa + '%' " +
                                                                                                                                            "or Hoten LIKE '%' + @tukhoa + '%' " +
                                                                                                                                            "or Diachi LIKE '%' + @tukhoa + '%' " +
                                                                                                                                            "or Gioitinh LIKE '%' + @tukhoa + '%' " +
                                                                                                                                            "or Ngaysinh LIKE '%' + @tukhoa + '%') and Xoa = 1", new object[] { tukhoa });
        }
        //Hàm Reset tài khoản mật khẩu mới
        public bool ResetAccount(string maNhanVien, string username, string password, out string error)
        {
            try
            {
                error = null;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Hash mật khẩu
                string query = "UPDATE Quanly SET Password = @Password , Username = @Username WHERE Manhanvien = @Manhanvien ";

                object[] parameters = { hashedPassword, username, maNhanVien };

                return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (SqlException e)
            {
                if (e.Number == 2627 || e.Number == 2601) error = "Username đã tồn tại";
                else error = "Lỗi SQL: " + e.Message;
                return false;
            }
        }


    }
}
