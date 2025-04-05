using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_CT_HDBH
    {
        public String maHangHoa { get; set; }

        public String maHoaDon { get; set; }
        public String tenHangHoa { get; set; }
        public String  soLuong { get; set; }
        public double tongTien { get; set; }
        public String barCode { get; set; }
        public List<DTO_CT_HDBH> CTHD { get; set; }


        public DTO_CT_HDBH(String maHoadon, String maHangHoa, String tenHangHoa,String soLuong, double tongTien, String barCode)
        {
            this.maHangHoa = maHangHoa;
            this.maHoaDon = maHoadon;
            this.tenHangHoa = tenHangHoa;
            this.soLuong = soLuong;
            this.tongTien = tongTien;
            this.barCode = barCode;
        }
    }
}
