using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Chamcong
    {
        public String ID  { get; set; }

        public DateTime thoiGianCN { get; set; }

        public TimeSpan checkIn { get; set; }

        public TimeSpan checkOut { get; set; }

        public double soCong { get; set; }

        public String trangThai { get; set; }
        public String maCaLam { get; set; }
        public String maNhanVien { get; set; }


        public List<DTO_Chamcong> chamCong { get; set; }


        public DTO_Chamcong(String ID, DateTime thoiGianCN,TimeSpan CheckIn, TimeSpan checkOut, double soCong, String trangThai, String maCaLam, String maNhanVien)
        {
            this.ID= ID;
            this.thoiGianCN = thoiGianCN;
            this.checkIn = CheckIn;
            this.checkOut = checkOut;
            this.soCong = soCong;
            this.trangThai = trangThai;
            this.maCaLam = maCaLam;
            this.maNhanVien = maNhanVien;
        }
    }
}
