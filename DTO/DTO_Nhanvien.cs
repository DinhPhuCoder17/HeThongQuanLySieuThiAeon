using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Nhanvien
    {
        public String maNhanvien { get; set; }
        public String hoTen { get; set; }
        public String cccd { get; set; }
        public DateTime ngaySinh { get; set; }
        public String gioiTinh { get; set; }
        public String diaChi { get; set; }
        public String soDienThoai { get; set; }
        public String vaiTro { get; set; }

        public List<DTO_Calam> lichLam { get; set; }
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

        public override bool Equals(object obj)
        {
            if (obj is DTO_Nhanvien other)
            {
                return this.maNhanvien == other.maNhanvien;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return hoTen.GetHashCode();
        }

        public DTO_Nhanvien() {
            lichLam = new List<DTO_Calam>();
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
