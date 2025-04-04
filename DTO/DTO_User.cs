using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_User
    {
        public string MaNhanvien { get; set; }
        public string Hoten { get; set; }
        public string Ngaysinh { get; set; }
        public string Gioitinh { get; set; }
        public string Diachi { get; set; }
        public string Sodienthoai { get; set; }
        public string Vaitro { get; set; }

        public DTO_User(string maNV, string hoTen, string ngaySinh, string gioiTinh, string diaChi, string sdt, string vaiTro)
        {
            MaNhanvien = maNV;
            Hoten = hoTen;
            Ngaysinh = ngaySinh;
            Gioitinh = gioiTinh;
            Diachi = diaChi;
            Sodienthoai = sdt;
            Vaitro = vaiTro;
        }
    }
}

