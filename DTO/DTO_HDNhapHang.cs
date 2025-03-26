using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HDNhapHang
    {
        public String soHD { get; set; }

        public int soLuong { get; set; }

        public String trangThai { get; set; }

        public DateTime ngayDat { get; set; }

        public float tongTien { get; set; }

        public List<DTO_HH_HDNH> CT_HDNH { get; set; }
        public DTO_HDNhapHang()
        {
            List<DTO_HH_HDNH> CT_HDNH = new List<DTO_HH_HDNH>();
        }
        public DTO_HDNhapHang(String soHD, int soLuong, String trangThai, DateTime ngayDat, float tongTien, List<DTO_HH_HDNH> CT_HDNH)
        {
            this.soHD = soHD;
            this.soLuong = soLuong;
            this.trangThai = trangThai;
            this.ngayDat = ngayDat;
            this.tongTien = tongTien;
            this.CT_HDNH = CT_HDNH;
        }
    }
}
