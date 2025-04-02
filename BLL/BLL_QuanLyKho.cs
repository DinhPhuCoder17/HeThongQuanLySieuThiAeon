using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BLL
{
    public class BLLQuanLyKho
    {
        private static BLLQuanLyKho instance;
        public static BLLQuanLyKho Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLLQuanLyKho();
                return instance;
            }
        }
        private BLLQuanLyKho() { }
        public List<DTO_Hanghoa> XemDSTonKho()
            {
                DataTable dt = DAL_QuanLyKho.Instance.XemDSTonKho();
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
        public DataTable XemCTHH(string mahh)
        {
            return DAL_QuanLyKho.Instance.XemCTHH(mahh);
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

            string sohdMoi = DAL_QuanLyKho.Instance.themMaHDNH(tongTien, tongSoLuong);
            if (string.IsNullOrEmpty(sohdMoi))
            {
                return false;
            }

            foreach (var item in listMaHangSoLuong)
            {
                bool isInserted = DAL_QuanLyKho.Instance.themHD_HH(item.Item1, sohdMoi, item.Item2);
                if (!isInserted)
                {
                    return false;
                }
            }

            return true;
        }

        public List<DTO_NhaCungCap> XemNCC()
        {
            DataTable dt = DAL_QuanLyKho.Instance.xemNCC(); 
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
                return DAL_QuanLyKho.Instance.ThemHangHoa(hangHoa);
         }

        // Auto update trạng thái nhập hàng
        public void AutoUpdateTrangThaiNhapHang()
        {
            DAL_QuanLyKho.Instance.AutoUpdateTrangThaiNhapHang();
        }

        // Xem danh sách nhập hàng
        public DataTable xemDSNH()
        {
            return DAL_QuanLyKho.Instance.xemDSNH();
        }

        // Hủy hóa đơn
        public Boolean huyHD(String soHD)
        {
            return DAL_QuanLyKho.Instance.huyHD(soHD);
        }

        public Boolean capNhatTTDH(DTO_HDNhapHang hDNhapHang)
        {
            return DAL_QuanLyKho.Instance.capNhatTTDH(hDNhapHang);
        }

        public DataTable xemCTDHBySohd(String soHD)
        {
            return DAL_QuanLyKho.Instance.xemCTDHBySohd(soHD);
        }

        public Boolean nhapKho(DTO_HDNhapHang hDNhapHang)
        {
            return DAL_QuanLyKho.Instance.nhapKho(hDNhapHang);
        }

        public DataTable xemDSKN(String soHD)
        {
            return DAL_QuanLyKho.Instance.xemDSKN(soHD);
        }

        public DataTable xemDSKNvaNCC(String soHD)
        {
            return DAL_QuanLyKho.Instance.xemDSKNvaNCC(soHD);
        }

        public DataTable xemDSNHvaNCC(String soHD)
        {
            return DAL_QuanLyKho.Instance.xemDSNHvaNCC(soHD);
        }

        public Boolean themKN(DTO_Khieunai kn)
        {
            return DAL_QuanLyKho.Instance.themKN(kn);
        }
        public DateTime xemNgayDatHang(String MaNH)
        {
            return DAL_QuanLyKho.Instance.xemNgayDatHang(MaNH);
        }


        public DataTable timKiemHDNH(String tukhoa)
        {
            return DAL_QuanLyKho.Instance.timKiemHDNH(tukhoa);
        }

        // -------------------------------- Nhà Cung cấp ------------------------------------
        public bool AddNCC(string tenNCC, string diaChi, string maSoThue, string sdt)
        {
            return DAL_QuanLyKho.Instance.AddNCC(tenNCC, diaChi, maSoThue, sdt);
        }
        public DataTable GetAllNCC()
        {
            return DAL_QuanLyKho.Instance.GetNCCList();
        }

        public bool UpdateNCC(string maNCC, string tenNCC, string maSoThue, string diaChi, string sdt)
        {
            return DAL_QuanLyKho.Instance.UpdateNCC(maNCC, tenNCC, maSoThue, diaChi, sdt);
        }

        public bool DeleteNCC(string maNCC)
        {
            return DAL_QuanLyKho.Instance.DeleteNCC(maNCC);
        }


    }
}
