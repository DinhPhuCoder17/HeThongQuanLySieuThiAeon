using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Nhanvien
    {
        public String maNhanvien;
        public String hoTen;
        public String cccd;
        public DateTime ngaySinh;
        public String gioiTinh;
        public String diaChi;
        public String soDienThoai;
        public DTO_Nhanvien(string maNhanvien, string hoTen, string cccd, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai)
        {
            this.maNhanvien = maNhanvien;
            this.hoTen = hoTen;
            this.cccd = cccd;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.diaChi = diaChi;
            this.soDienThoai = soDienThoai;
        }
        public string MaNhanvien { get => maNhanvien; set => maNhanvien = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string Cccd { get => cccd; set => cccd = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
    }
}
