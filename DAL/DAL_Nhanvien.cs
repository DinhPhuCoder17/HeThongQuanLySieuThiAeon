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

        //Hàm thêm nhân viên (làm cho cả thêm quản lý)
        public bool InsertEmployee(string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string sdt, string username, string password, string role)
        {

            // Gọi stored procedure và lấy mã nhân viên mới
            string query = "EXEC themMaNhanvien @HoTen , @CCCD , @NgaySinh , @GioiTinh , @DiaChi , @Sodienthoai ";
            object[] parameters =
            {
            hoTen,
            SecurityHelper.Encrypt(cccd), // Mã hóa CCCD
            ngaySinh,
            gioiTinh,
            diaChi,     
            SecurityHelper.Encrypt(sdt) //Mã hoá SĐT
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

        public DataTable GetEmployeeList()
        {
            string query = @"
        SELECT nv.Manhanvien, nv.Hoten, nv.CCCD, nv.Ngaysinh, nv.Gioitinh, nv.Diachi, nv.Sodienthoai,
            CASE 
                WHEN nv.Manhanvien IN (SELECT Manhanvien FROM Quanly WHERE Role = 'Admin') THEN N'Admin'
                WHEN nv.Manhanvien IN (SELECT Manhanvien FROM Quanly WHERE Role = 'Kho') THEN N'Quản lý kho'
                WHEN nv.Manhanvien IN (SELECT Manhanvien FROM Quanly WHERE Role = 'TCNS') THEN N'Quản lý nhân sự'
                ELSE 'Nhân viên'
            END AS VaiTro
        FROM Nhanvien nv
        WHERE nv.Xoa = 1";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Update Nhan vien
        public bool UpdateEmployee(string maNhanVien, string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai)
        {
            string query = "UPDATE Nhanvien SET Hoten = @Hoten , CCCD = @CCCD , Ngaysinh = @Ngaysinh , Gioitinh = @Gioitinh , Diachi = @Diachi , Sodienthoai = @Sodienthoai WHERE Manhanvien = @Manhanvien ";

            object[] parameters = { hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai, maNhanVien };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        //Delete Nhan vien
        public bool DeleteEmployee(string maNhanVien)
        {
            string query = "UPDATE Nhanvien SET Xoa = 0 WHERE Manhanvien = @Manhanvien ";
            object[] parameters = { maNhanVien };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }


    }
}
