﻿using System;
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
        public string UuDai { get; set; }
        public string NhaCC { get; set; } 
        public DateTime NgayHetHan { get; set; } // ko có trong database

        public string DanhMuc { get; set; }
        public int THSD { get; set; }
        // Constructor không tham số
        public DTO_Hanghoa() { }

        // Constructor có tham số
        public DTO_Hanghoa(string maHangHoa, string tenHangHoa, float giaNhap, float giaBan,
                          byte[] hinhAnh, int soLuong, string uuDai, string nhaCC, DateTime ngayHetHan, string DM, int HSD)
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
            THSD = HSD;
            DanhMuc = DM;
        }
    }
}
