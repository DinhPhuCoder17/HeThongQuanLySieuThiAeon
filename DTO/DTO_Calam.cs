using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Calam
    {
        public  String maCaLam { get; set; }
        public  String tenCaLam { get; set; }
        public  DateTime tgBatDau { get; set; }

        public  DateTime tgKetThuc { get; set; }

        public  int soLuongNhanVien { get; set; }

        public  List<String> PC_Nhanvien { get; set; }

        public DTO_Calam(string maCaLam, string tenCaLam, DateTime tgBatDau, DateTime tgKetThuc, int soLuongNhanVien, List<String> pC_Nhanvien)
        {
            this.maCaLam = maCaLam;
            this.tenCaLam = tenCaLam;
            this.tgBatDau = tgBatDau;
            this.tgKetThuc = tgKetThuc;
            this.soLuongNhanVien = soLuongNhanVien;
            PC_Nhanvien = pC_Nhanvien;
        }
    }
}
