using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Hanghoa
    {
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public float GiaNhap { get; set; }
        public float GiaBan { get; set; }
        public byte[] HinhAnh { get; set; } 
        public int SoLuong { get; set; }
        public float UuDai { get; set; }
        public string NhaCC { get; set; } 
        public DateTime NgayHetHan { get; set; }

        // Constructor không tham số
        public DTO_Hanghoa() { }

        // Constructor có tham số
        public DTO_Hanghoa(string maHangHoa, string tenHangHoa, float giaNhap, float giaBan,
                          byte[] hinhAnh, int soLuong, float uuDai, string nhaCC, DateTime ngayHetHan)
        {
            MaHangHoa = maHangHoa;
            TenHangHoa = tenHangHoa;
            GiaNhap = giaNhap;
            GiaBan = giaBan;
            HinhAnh = hinhAnh;
            SoLuong = soLuong;
            UuDai = uuDai;
            NhaCC = nhaCC;
            NgayHetHan = ngayHetHan;
        }
    }
}
