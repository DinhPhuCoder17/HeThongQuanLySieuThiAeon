using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class DAL_QuanLyKho
    {
        public static DataTable hangHoa_NhapHang()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.TenNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1");
        }
        public bool datHang(DTO_Hanghoa hh)
        {

           /* int line = DataProvider.Instance.ExecuteNonQuery(
                "exec tthemMaHDNH @Sodienthoai , @Hoten , @Diachi , @Gioitinh", new object[] { hh.soDienThoai, kh.hoTen, kh.diaChi, kh.gioiTinh }
            );
            if (line != 0)
            {
                return true;
            }*/
            return false;
        }
        public void AutoUpdateTrangThaiNhapHang()
        {
            try
            {
                DataProvider.Instance.ExecuteNonQuery("UPDATE HD_Nhaphang SET TrangThai = N'Đang Vận Chuyển' WHERE TrangThai = N'Chờ Xác Nhận'  AND DATEDIFF(HOUR, Ngaydat, GETDATE()) >= 2");
            }
            catch
            {

            }
        }

        public DataTable xemDSNH()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Sohd, Ngaydat, Tongtien, Trangthai FROM HD_Nhaphang");
        }
    }
}
