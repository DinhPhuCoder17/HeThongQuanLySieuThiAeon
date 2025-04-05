using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL; 

namespace BLL
{
    public class BLL_Nhanvien
    {
        private static BLL_Nhanvien instance;
        public static BLL_Nhanvien Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL_Nhanvien();
                return instance;
            }
        }
        private BLL_Nhanvien() { }

        public bool AddEmployee(string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string sdt, string rolenv, string username, string password, string role)
        {
            return DAL_Nhanvien.Instance.InsertEmployee(hoTen, cccd, ngaySinh, gioiTinh, diaChi, sdt, rolenv, username, password, role);
        }

        public DataTable GetAllEmployees()
        {
            return DAL_Nhanvien.Instance.GetEmployeeList();
        }

        public bool UpdateEmployee(string maNhanVien, string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai, string vaiTro)
        {
            return DAL_Nhanvien.Instance.UpdateEmployee(maNhanVien, hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai, vaiTro);
        }

        public bool DeleteEmployee(string maNhanVien)
        {
            return DAL_Nhanvien.Instance.DeleteEmployee(maNhanVien);
        }

        public bool AddManager(string maNhanVien, string userName, string passWord, string role)
        {
            switch (role)
            {
                case "Quản Lý Tài Chính Nhân Sự":
                    role = "TCNS";
                    break;
                case "Admin":
                    role = "Admin";
                    break;
                case "Quản Lý Kho":
                    role = "Kho";
                    break;
            }
            return DAL_Nhanvien.Instance.AddManager(maNhanVien, userName, passWord, role);
        }

        public DataTable GetEmployeeById(string maNV)
        {
            return DAL_Nhanvien.Instance.GetEmployeeInfo(maNV);
        }
        public bool UpdatePassword(string maNhanVien, string oldpassword, string password)
        {
            return DAL_Nhanvien.Instance.UpdatePassword(maNhanVien, oldpassword, password);
        }

        public bool IsCCCDExist(string cccd)
        {
            return DAL_Nhanvien.Instance.IsCCCDExist(cccd);
        }

        public bool IsPhoneExist(string sdt)
        {
            return DAL_Nhanvien.Instance.IsPhoneExist(sdt);
        }

    }
}
