using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace DAL
{
    public class DAL_QuanlyTCNS
    {

        //Xem danh sách nhân viên
        public DataTable xemDSKH()
        {
            return DataProvider.Instance.ExecuteQuery("Select Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang From Khachhang where Xoa = 1");
        }


        //Xem danh sách khách hàng
        public DataTable xemDSNV()
        {
            return DataProvider.Instance.ExecuteQuery("Select Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai From Nhanvien where Xoa = 1");
        }

        //Thêm khách hàng
        public bool themKH(DTO_Khachhang kh)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery(
                    "exec themKH @Sodienthoai , @Hoten , @Diachi , @Gioitinh", new object[] { kh.soDienThoai, kh.hoTen, kh.diaChi, kh.gioiTinh }
                );
                if (line != 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Khách hàng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        //Xóa khách hàng
        public bool xoaKH(String soDienThoai)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery("Update Khachhang set Xoa = 0 Where Sodienthoai = @Sodienthoai", new object[] { soDienThoai });
                if (line != 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
