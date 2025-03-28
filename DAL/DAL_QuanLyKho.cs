﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace DAL
{
    public class DAL_QuanLyKho
    {
        public DataTable XemDSTonKho()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.MaNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1");
        }
        public static DataTable hangHoa_NhapHang()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.TenNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1");
        }

        public string themMaHDNH(double tongTien, int tongSoLuong)
        {
            string queryThemMaHDNH = "EXEC themMaHDNH @Tongtien , @Soluong";
            int rowAffected = DataProvider.Instance.ExecuteNonQuery(queryThemMaHDNH, new object[] { tongTien, tongSoLuong });
            if (rowAffected <= 0)
            {
                return null;
            }

            string queryGetSohd = "SELECT MAX(Sohd) FROM HD_Nhaphang";
            object sohdObj = DataProvider.Instance.ExecuteScalar(queryGetSohd);
            if (sohdObj == null)
                return null;

            return sohdObj.ToString();
        }

        public bool themHD_HH(string maHangHoa, string sohd, int soLuongDat)
        {
            string queryThemHD_HH = "EXEC themHD_HH @Mahanghoa , @Sohd , @Soluongdat";
            int rowAffected = DataProvider.Instance.ExecuteNonQuery(queryThemHD_HH, new object[] { maHangHoa, sohd, soLuongDat });

            return (rowAffected > 0);
        }

        public DataTable xemNCC()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT MaNCC, TenNCC, Diachi, Masothue, Sodienthoai FROM Nhacungcap WHERE Xoa = 1;");
        }
        public void AutoUpdateTrangThaiNhapHang()
        {
            try
            {
                DataProvider.Instance.ExecuteNonQuery("UPDATE HD_Nhaphang SET TrangThai = N'Đang Vận Chuyển' WHERE TrangThai = N'Chờ Xác Nhận'  AND DATEDIFF(HOUR, Ngaydat, GETDATE()) >= 2");
            }
            catch
            {

            }
        }
        public bool ThemHangHoa(DTO_Hanghoa hangHoa)
        {
            string query = "EXEC themMaHanghoa @Tenhanghoa , @Tiennhap , @Tendanhmuc , @Tienban , @ImageData , @Soluong , @Uudai , @MaNCC , @THSD";

            int rowAffected = DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
        hangHoa.TenHangHoa,
        hangHoa.GiaNhap,   // Giá nhập
        hangHoa.DanhMuc,   
        hangHoa.GiaBan,    
        hangHoa.HinhAnh,  
        0,                 
        "0%",             
        hangHoa.NhaCC,     
        hangHoa.THSD       
            });

            return rowAffected > 0;
        }



        public DataTable xemDSNH()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Sohd, Ngaydat, Tongtien, Trangthai FROM HD_Nhaphang");
        }

        public Boolean huyHD(String soHD)
        {
            try
            {
                int line = 0;
                int lineNext = 0;
                line = DataProvider.Instance.ExecuteNonQuery("DELETE FROM HD_HH WHERE Sohd = @soHD ", new object[] { soHD });
                if(line != 0)
                {
                    lineNext = DataProvider.Instance.ExecuteNonQuery("DELETE FROM HD_NhapHang WHERE Sohd = @soHD ", new object[] { soHD });
                    if(lineNext != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Boolean capNhatTTDH(DTO_HDNhapHang hDNhapHang)
        {
            try
            {
                switch (hDNhapHang.trangThai)
                {
                    case "Đang Vận Chuyển":
                        return DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Kiểm Kho' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD }) != 0;
                    case "Chờ Xác Nhận":
                        return DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Đang Vận Chuyển' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD }) != 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public DataTable xemCTDHBySohd(String soHD)
        {
            return DataProvider.Instance.ExecuteQuery("Select Sohd, hh.Mahanghoa, Tenhanghoa, Ngaynhap, Soluongdat, Soluongnhan, Ngaysanxuat, Hansudung, Thanhtien, THSD, Trangthai From HD_HH join Hanghoa hh on HD_HH.Mahanghoa = hh.Mahanghoa Where Sohd = @soHD ", new object[] { soHD });
        }

        public Boolean nhapKho(DTO_HDNhapHang hDNhapHang)
        {
            try
            {

                int lineFirst = DataProvider.Instance.ExecuteNonQuery("Update HD_Nhaphang set Trangthai = @Trangthai where Sohd = @Sohd ", new object[] {hDNhapHang.trangThai, hDNhapHang.soHD});
                foreach (DTO_HH_HDNH hh in hDNhapHang.CT_HDNH)
                {
                    int line = DataProvider.Instance.ExecuteNonQuery("UPDATE Hanghoa SET Soluong = Soluong + @soluongnhan  WHERE Mahanghoa = @mahanghoa", new object[] { hh.SoLuongNhan > hh.SoLuongDat ? hh.SoLuongDat: hh.SoLuongNhan, hh.HangHoa.MaHangHoa });
                    if(line != 0)
                    {
                        int lineNext = DataProvider.Instance.ExecuteNonQuery("UPDATE HD_HH SET Soluongnhan = @soluongnhan , Ngaysanxuat = @Ngaysanxuat , Hansudung = @Hansudung , Trangthai = @Trangthai  WHERE Mahanghoa = @mahanghoa", new object[] { hh.SoLuongNhan, hh.NSX, hh.HSD, hh.TrangThai, hh.HangHoa.MaHangHoa });
                        if (lineNext == 0)
                        {
                            return false;
                        }
                    }else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable xemDSKN(String soHD)
        {
            return DataProvider.Instance.ExecuteQuery("Select HD_HH.Sohd, HD_HH.Mahanghoa, Tenhanghoa, Ngaynhap, Soluongdat, Soluongnhan, Luongchenhlech, Loaikhieunai, Lydochitiet From HD_HH left join Khieunai KN on HD_HH.Mahanghoa = KN.Mahanghoa and HD_HH.Sohd = KN.Sohd left join Hanghoa HH on HD_HH.Mahanghoa = HH.Mahanghoa Where HD_HH.Sohd = @Sohd", new object[] {soHD});
        }

        public Boolean themKN(DTO_Khieunai kn)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery("Exec themKhieuNai @Mahanghoa , @Sohd , @Loaikhieunai , @Lydochitiet , @Luongchenhlech ", new object[] { kn.MaHH, kn.SoHD, kn.Loaikhieunai, kn.Lydochitiet, kn.Luongchenhlech});
                if(line != 0)
                {
                    return true;
                }
                return false;
            }

            catch {
                MessageBox.Show("Không tồn tại hóa đơn này!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; 
            }
        }
    }
}
