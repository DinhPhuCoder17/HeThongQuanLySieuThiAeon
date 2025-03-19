using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_predictorHelper
    {
        public string Mahanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public decimal Tiennhap { get; set; }
        public string Hang { get; set; }

        public DTO_predictorHelper()
        {
        }
        public DTO_predictorHelper(string mahanghoa, string tenhanghoa, string maNCC, string tenNCC, decimal tiennhap, string hang)
        {
            Mahanghoa = mahanghoa;
            Tenhanghoa = tenhanghoa;
            MaNCC = maNCC;
            TenNCC = tenNCC;
            Tiennhap = tiennhap;
            Hang = hang;
        }
    }
}
