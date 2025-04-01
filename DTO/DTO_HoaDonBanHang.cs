using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HoaDonBanHang
    {
        public String maHoaDon { get; set; }
        public DateTime thoiGianBan { get; set; }
        public String maNhanVien{ get; set; }
        public int soDienThoai { get; set; }
        public double thanhTien { get; set; }

        public List<DTO_HoaDonBanHang> hoaDonBanHang { get; set; }


        public DTO_HoaDonBanHang(String maHoaDon, DateTime thoiGianBan, String maNhanVien, int soDienThoai, double thanhTien)
        {
           this.maHoaDon = maHoaDon;
            this.thoiGianBan = thoiGianBan;
            this.maNhanVien = maNhanVien;
            this.soDienThoai = soDienThoai;
            this.thanhTien = thanhTien;
        }
    }
}
