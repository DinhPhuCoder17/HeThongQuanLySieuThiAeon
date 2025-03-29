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
        public int MaSoThue { get; set; }

        public DTO_NhaCungCap() { }
        public DTO_NhaCungCap(string maNCC, string tenNCC, string soDT, int maSoThue)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            SoDT = soDT;
            MaSoThue = maSoThue;
        }
    }

}
