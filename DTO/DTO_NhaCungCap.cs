using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_NhaCungCap
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string SoDT { get; set; }
        public string MaSoThue { get; set; }
        public string Diachi { get; set; }

        public DTO_NhaCungCap() { }
        public DTO_NhaCungCap(string maNCC, string tenNCC, string soDT, string maSoThue, string diaChi)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            SoDT = soDT;
            MaSoThue = maSoThue;
            Diachi = diaChi;
        }
    }

}
