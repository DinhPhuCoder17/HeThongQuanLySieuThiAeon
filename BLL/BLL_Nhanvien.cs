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

        public bool AddEmployee(string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string sdt, string username, string password, string role)
        {
            return DAL_Nhanvien.Instance.InsertEmployee(hoTen, cccd, ngaySinh, gioiTinh, diaChi, sdt, username, password, role);
        }

        public DataTable GetAllEmployees()
        {
            return DAL_Nhanvien.Instance.GetEmployeeList();
        }

        public bool UpdateEmployee(string maNhanVien, string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai)
        {
            return DAL_Nhanvien.Instance.UpdateEmployee(maNhanVien, hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai);
        }

        public bool DeleteEmployee(string maNhanVien)
        {
            return DAL_Nhanvien.Instance.DeleteEmployee(maNhanVien);
        }

    }
}
