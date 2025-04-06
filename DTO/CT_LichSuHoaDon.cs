using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class CT_LichSuHoaDon
    {
        public string MaHoaDon { get; set; }   // Mã hóa đơn
        public DateTime ThoigianXoa { get; set; } // Thời gian xóa hóa đơn
        public string LidoXoa { get; set; }  // Lý do xóa hóa đơn
        public string Mahanghoa { get; set; } // Mã hàng hóa
        public string Tenhanghoa { get; set; } // Tên hàng hóa
        public int Soluong { get; set; }  // Số lượng sản phẩm
        public decimal Tongtien { get; set; } // Tổng tiền của sản phẩm trong hóa đơn
        public string Sodienthoai { get; set; } // Số điện thoại của khách hàng

        // Constructor
        public CT_LichSuHoaDon(string maHoaDon, DateTime thoigianXoa, string lidoXoa,
                                        string mahanghoa, string tenhanghoa, int soluong,
                                        decimal tongtien, string sodienthoai)
        {
            MaHoaDon = maHoaDon;
            ThoigianXoa = thoigianXoa;
            LidoXoa = lidoXoa;
            Mahanghoa = mahanghoa;
            Tenhanghoa = tenhanghoa;
            Soluong = soluong;
            Tongtien = tongtien;
            Sodienthoai = sodienthoai;
        }
    }
}
