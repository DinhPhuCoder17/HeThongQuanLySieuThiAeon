using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_QuanLyKho
    {
        public static DataTable hangHoa_NhapHang()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.TenNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1");
        }
    }
}
