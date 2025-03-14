using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Khachhang
    {

        public String hoTen { get; set; }

        public String soDienThoai { get; set; }

        public String gioiTinh { get; set; }

        public String diaChi { get; set; }

        public int diemThuong { get; set; }

        public String hang { get; set; }

        public List<DTO_HDBanHang> hDBanhang { get; set; }


        public DTO_Khachhang(string hoTen, string soDienThoai, string gioiTinh, string diaChi, int diemThuong, string hang, List<DTO_HDBanHang> hDBanhang)
        {
            this.hoTen = hoTen;
            this.soDienThoai = soDienThoai;
            this.gioiTinh = gioiTinh;
            this.diaChi = diaChi;
            this.diemThuong = diemThuong;
            this.hang = hang;
            this.hDBanhang = hDBanhang;
        }



    }
}
