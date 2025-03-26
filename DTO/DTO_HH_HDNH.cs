using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HH_HDNH
    {
        public DTO_Hanghoa HangHoa { get; set; }  
        public int SoLuongDat { get; set; }
        public int SoLuongNhan { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime HanThanhToan { get; set; }
        public DateTime NSX { get; set; } 
        public string HSD { get; set; } 

        public DTO_HH_HDNH() { }
        public DTO_HH_HDNH(DTO_Hanghoa hangHoa, int soLuongDat, int soLuongNhan, DateTime ngayNhan,
                           DateTime hanThanhToan, DateTime nsx, string hsd)
        {
            HangHoa = hangHoa;
            SoLuongDat = soLuongDat;
            SoLuongNhan = soLuongNhan;
            NgayNhan = ngayNhan;
            HanThanhToan = hanThanhToan;
            NSX = nsx;
            HSD = hsd;
        }
    }
}
