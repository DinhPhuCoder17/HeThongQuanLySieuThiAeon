﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace BLL
{
    public class BLLQuanLyKho
    {
        private readonly DAL_QuanLyKho dAL_QuanLyKho = new DAL_QuanLyKho();
            public List<DTO_Hanghoa> XemDSTonKho()
            {
                DataTable dt = dAL_QuanLyKho.XemDSTonKho();
                List<DTO_Hanghoa> dsHangHoa = new List<DTO_Hanghoa>();

                foreach (DataRow dr in dt.Rows)
                {
                    DTO_Hanghoa _list = new DTO_Hanghoa
                    {
                        MaHangHoa = dr["MaHangHoa"].ToString(),
                        TenHangHoa = dr["TenHangHoa"].ToString(),
                        GiaNhap = float.Parse(dr["TienNhap"].ToString()),
                        GiaBan = float.Parse(dr["TienBan"].ToString()),
                        HinhAnh = dr["ImageData"] == DBNull.Value ? null : (byte[])dr["ImageData"],
                        SoLuong = int.Parse(dr["SoLuong"].ToString()),
                        UuDai = dr["UuDai"].ToString(),
                        NhaCC = dr["MaNCC"].ToString(),
                        DanhMuc = dr["TenDanhMuc"].ToString(),
                        THSD = (int)dr["THSD"]
                    };

                    dsHangHoa.Add(_list);
                }         
     
                return dsHangHoa;
            }
            public List<DTO_Hanghoa> hangHoa_NhapHang()
            {
            DataTable dt = DAL_QuanLyKho.hangHoa_NhapHang();
            List<DTO_Hanghoa> list = new List<DTO_Hanghoa>();

            string defaultImagePath = "Resources/z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg";
            byte[] defaultImage = File.Exists(defaultImagePath) ? File.ReadAllBytes(defaultImagePath) : null;

            foreach (DataRow row in dt.Rows)
            {
                DTO_Hanghoa hangHoa = new DTO_Hanghoa
                {
                    MaHangHoa = row["MaHangHoa"].ToString(),
                    DanhMuc = row["TenDanhMuc"].ToString(),
                    TenHangHoa = row["Tenhanghoa"].ToString(),
                    NhaCC = row["TenNCC"].ToString(),
                    GiaNhap = Convert.ToSingle(row["Tiennhap"]),
                    HinhAnh = row["ImageData"] != DBNull.Value ? (byte[])row["ImageData"] : defaultImage
                };

                list.Add(hangHoa);
            }

            return list;
        }
        public bool datHang(
         List<DTO_HH_HDNH> dsChiTiet,
        out int tongSoLuong,
        out double tongTien,
        out List<Tuple<string, int>> listMaHangSoLuong)
        {
            tongSoLuong = 0;
            tongTien = 0;
            listMaHangSoLuong = new List<Tuple<string, int>>();

            if (dsChiTiet == null || dsChiTiet.Count == 0)
                return false;

            foreach (var ct in dsChiTiet)
            {
                tongSoLuong += ct.SoLuongDat;
                tongTien += ct.SoLuongDat * ct.HangHoa.GiaNhap;
                listMaHangSoLuong.Add(new Tuple<string, int>(ct.HangHoa.MaHangHoa, ct.SoLuongDat));
            }

            string sohdMoi = dAL_QuanLyKho.themMaHDNH(tongTien, tongSoLuong);
            if (string.IsNullOrEmpty(sohdMoi))
            {
                return false;
            }

            foreach (var item in listMaHangSoLuong)
            {
                bool isInserted = dAL_QuanLyKho.themHD_HH(item.Item1, sohdMoi, item.Item2);
                if (!isInserted)
                {
                    return false;
                }
            }

            return true;
        }

        public List<DTO_NhaCungCap> XemNCC()
        {
            DataTable dt = dAL_QuanLyKho.xemNCC(); 
            List<DTO_NhaCungCap> list = new List<DTO_NhaCungCap>();

            foreach (DataRow row in dt.Rows)
            {
                DTO_NhaCungCap ncc = new DTO_NhaCungCap
                {
                    MaNCC = row["MaNCC"].ToString(),
                    TenNCC = row["TenNCC"].ToString()
                };

                list.Add(ncc);
            }

            return list;
        }

         public bool ThemMatHang(DTO_Hanghoa hangHoa)
         {
                return dAL_QuanLyKho.ThemHangHoa(hangHoa);
         }

        // Auto update trạng thái nhập hàng
        public void AutoUpdateTrangThaiNhapHang()
        {
            dAL_QuanLyKho.AutoUpdateTrangThaiNhapHang();
        }

        // Xem danh sách nhập hàng
        public DataTable xemDSNH()
        {
            return dAL_QuanLyKho.xemDSNH();
        }

        // Hủy hóa đơn
        public Boolean huyHD(String soHD)
        {
            return dAL_QuanLyKho.huyHD(soHD);
        }

        public Boolean capNhatTTDH(DTO_HDNhapHang hDNhapHang)
        {
            return dAL_QuanLyKho.capNhatTTDH(hDNhapHang);
        }

        public DataTable xemCTDHBySohd(String soHD)
        {
            return dAL_QuanLyKho.xemCTDHBySohd(soHD);
        }

        public Boolean nhapKho(DTO_HDNhapHang hDNhapHang)
        {
            return dAL_QuanLyKho.nhapKho(hDNhapHang);
        }

        public DataTable xemDSKN(String soHD)
        {
            return dAL_QuanLyKho.xemDSKN(soHD);
        }

        public Boolean themKN(DTO_Khieunai kn)
        {
            return dAL_QuanLyKho.themKN(kn);
        }
    }
}
