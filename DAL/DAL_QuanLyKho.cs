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
    public class DAL_QuanLyKho
    {
        public static DataTable hangHoa_NhapHang()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.TenNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1");
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

        public Boolean huyHD(String soHD)
        {
            try
            {
                int line = 0;
                int lineNext = 0;
                line = DataProvider.Instance.ExecuteNonQuery("DELETE FROM HD_HH WHERE Sohd = @soHD ", new object[] { soHD });
                if(line != 0)
                {
                    lineNext = DataProvider.Instance.ExecuteNonQuery("DELETE FROM HD_NhapHang WHERE Sohd = @soHD ", new object[] { soHD });
                    if(lineNext != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Boolean capNhatTTDH(DTO_HDNhapHang hDNhapHang)
        {
            try
            {
                switch (hDNhapHang.trangThai)
                {
                    case "Đang Vận Chuyển":
                        return DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Kiểm Kho' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD }) != 0;
                    case "Chờ Xác Nhận":
                        return DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Đang Vận Chuyển' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD }) != 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public DataTable xemCTDHBySohd(String soHD)
        {
            return DataProvider.Instance.ExecuteQuery("Select * From HD_HH Where Sohd = @soHD ", new object[] { soHD });
        }
    }
}
