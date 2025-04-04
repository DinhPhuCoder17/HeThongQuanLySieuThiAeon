using DTO;
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
        private static DAL_QuanLyKho instance;
        public static DAL_QuanLyKho Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_QuanLyKho();
                return instance;
            }
        }
        private DAL_QuanLyKho() { }
        public DataTable XemDSTonKho()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.MaNCC, h.THSD, h.Barcode FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1 and h.Soluong > = 0");
        }
        public DataTable xemBarcode() 
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Barcode FROM Hanghoa WHERE Xoa = 0");
        }
        public bool KhoiPhucHangHoa(string barcode)
        {
            string query = "UPDATE HangHoa SET Xoa = 1 WHERE Barcode = @Barcode";
            object[] parameters = new object[] { barcode };
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
        public bool XoaHangHoa(string barcode)
        {
            string query = "UPDATE HangHoa SET Xoa = 0 WHERE Barcode = @Barcode";
            object[] parameters = new object[] { barcode };
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
        public bool UpdateHanghoa(DTO_Hanghoa hangHoa)
        {
            // Câu lệnh SQL để cập nhật thông tin hàng hóa, bao gồm hình ảnh
            string query = "UPDATE HangHoa SET TenHangHoa = @TenHangHoa , TenDanhMuc = @TenDanhMuc , Tiennhap = @GiaNhap , TienBan = @GiaBan , ImageData = @ImageData , THSD = @THSD WHERE Barcode = @Barcode";

            // Thêm tham số vào câu lệnh SQL
            object[] parameters = new object[]
            {
        hangHoa.TenHangHoa,
        hangHoa.DanhMuc,
        hangHoa.GiaNhap,
        hangHoa.GiaBan,
        hangHoa.HinhAnh, // Đây là ảnh dưới dạng byte[]
        hangHoa.THSD,
        hangHoa.Barcode
            };

            // Thực thi câu lệnh SQL thông qua phương thức ExecuteNonQuery
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);

            // Nếu có ít nhất một hàng bị ảnh hưởng, trả về true (thành công)
            return result > 0;
        }

        public bool SuaHangHoa(string barcode, string tenHangHoa, string danhMuc, float giaNhap, float giaBan, int thsd, string nhaCC, int soLuong)
        {
            string query = "UPDATE HangHoa SET TenHangHoa = @TenHangHoa , TenDanhMuc = @DanhMuc , TienNhap = @GiaNhap , TienBan = @GiaBan , THSD = @THSD , MaNCC = @NhaCC , SoLuong = @SoLuong WHERE Barcode = @Barcode";
            object[] parameters = new object[] { tenHangHoa, danhMuc, giaNhap, giaBan, thsd, nhaCC, soLuong, barcode };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);

            return result > 0;
        }

        public static DataTable hangHoa_NhapHang()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.TenNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1");
        }

        public DataTable XemCTHH(string mahh)
        {
            string query = @"SELECT Mahanghoa, NgaySanXuat, Hansudung, Soluongnhan FROM HD_HH WHERE Mahanghoa = @mahh AND Trangthai = N'Đã Nhập Kho'";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { mahh });
            return dt;
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
            string query = "EXEC themMaHanghoa @Tenhanghoa , @Tiennhap , @Tendanhmuc , @Tienban , @ImageData , @Soluong , @Uudai , @MaNCC , @THSD , @Barcode";

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
        hangHoa.THSD,     
        hangHoa.Barcode
            });

            return rowAffected > 0;
        }



        public DataTable xemDSNH()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Sohd, Ngaydat, FORMAT(Tongtien, 'C', 'vi-VN') AS Tongtien, Trangthai FROM HD_Nhaphang Order by Ngaydat desc");
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
                        return DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Kiểm Kê' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD }) != 0;
                    case "Chờ Xác Nhận":
                        return DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Đang Vận Chuyển' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD }) != 0;
                    case "Chờ Xử Lý Bổ Sung":
                        DataTable kn = DataProvider.Instance.ExecuteQuery("Select * From Khieunai Where Sohd = @Sohd ", new object[] {hDNhapHang.soHD});
                        if (kn == null || kn.Rows.Count == 0)  // Kiểm tra null hoặc DataTable không có dữ liệu
                        {
                            MessageBox.Show("Chưa xử lý khiếu nại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        int FirstCommand = DataProvider.Instance.ExecuteNonQuery("UPDATE HD_NhapHang SET TrangThai = N'Đã Xử Lý' WHERE Sohd = @soHD", new object[] { hDNhapHang.soHD});
                        if (FirstCommand != 0)
                        {
                            DataTable dt = DataProvider.Instance.ExecuteQuery("Select HD_HH.Mahanghoa, hh.soluong - (Soluongnhan - Soluongdat) as Hieu From HD_HH left join HangHoa hh on HD_HH.Mahanghoa = hh.Mahanghoa Where Sohd = @Sohd AND Soluongnhan < Soluongdat", new object[] { hDNhapHang.soHD });
                            foreach(DataRow row in dt.Rows)
                            {
                                DataProvider.Instance.ExecuteNonQuery("Update Hanghoa set Soluong = @Soluong Where Mahanghoa = @Mahanghoa ", new object[] { int.Parse(row[1].ToString()), row[0].ToString() });
                            }
                            DataProvider.Instance.ExecuteNonQuery("Update HD_HH set Soluongnhan = Soluongdat Where Sohd = @Sohd", new object[] { hDNhapHang.soHD });
                            return true;
                        }
                        return false;
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
            return DataProvider.Instance.ExecuteQuery("Select Sohd, hh.Mahanghoa, Tenhanghoa, Ngaynhap, Soluongdat, Soluongnhan, Ngaysanxuat, Hansudung, FORMAT(Thanhtien, 'C', 'vi-VN') AS Thanhtien, THSD, Trangthai From HD_HH join Hanghoa hh on HD_HH.Mahanghoa = hh.Mahanghoa Where Sohd = @soHD ", new object[] { soHD });
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
                        int lineNext = DataProvider.Instance.ExecuteNonQuery("UPDATE HD_HH SET Soluongnhan = @soluongnhan , Ngaysanxuat = @Ngaysanxuat , Hansudung = @Hansudung , Trangthai = @Trangthai , Ngaynhap = @Ngaynhap  WHERE Mahanghoa = @mahanghoa and Sohd = @Sohd", new object[] { hh.SoLuongNhan, hh.NSX, hh.HSD, hh.TrangThai, hh.NgayNhan, hh.HangHoa.MaHangHoa, hDNhapHang.soHD });
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
            return DataProvider.Instance.ExecuteQuery("Select HD_HH.Sohd, HD_HH.Mahanghoa, Tenhanghoa, Ngaynhap, Soluongdat, Soluongnhan, Luongchenhlech, Loaikhieunai, Lydochitiet, Yeucauxuly From HD_HH left join Khieunai KN on HD_HH.Mahanghoa = KN.Mahanghoa and HD_HH.Sohd = KN.Sohd left join Hanghoa HH on HD_HH.Mahanghoa = HH.Mahanghoa Where HD_HH.Sohd = @Sohd", new object[] {soHD});
        }

        public DataTable xemDSKNvaNCC(String soHD)
        {
            return DataProvider.Instance.ExecuteQuery("Select HD_HH.Sohd, HD_HH.Mahanghoa, Tenhanghoa, TenNCC, Ngaynhap, Soluongdat, Soluongnhan, Luongchenhlech, Loaikhieunai, Lydochitiet, Yeucauxuly From HD_HH left join Khieunai KN on HD_HH.Mahanghoa = KN.Mahanghoa and HD_HH.Sohd = KN.Sohd left join Hanghoa HH on HD_HH.Mahanghoa = HH.Mahanghoa left join Nhacungcap NCC on HH.Mancc = NCC.Mancc  Where HD_HH.Sohd = @Sohd", new object[] { soHD });
        }

        public DataTable xemDSNHvaNCC(String soHD)
        {
            return DataProvider.Instance.ExecuteQuery("Select HD_HH.Sohd, HD_HH.Mahanghoa, Tenhanghoa, TenNCC, Tiennhap, Soluongdat, Thanhtien From HD_HH left join Hanghoa HH on HD_HH.Mahanghoa = HH.Mahanghoa left join Nhacungcap NCC on HH.Mancc = NCC.Mancc  Where HD_HH.Sohd = @Sohd", new object[] { soHD });
        }

        public Boolean themKN(DTO_Khieunai kn)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery("Exec themKhieuNai @Mahanghoa , @Sohd , @Loaikhieunai , @Lydochitiet , @Luongchenhlech , @Yeucauxuly", new object[] { kn.MaHH, kn.SoHD, kn.Loaikhieunai, kn.Lydochitiet, kn.Luongchenhlech, kn.Yeucauxuly});
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

        //Liemp
        public DateTime xemNgayDatHang(String maNH)
        {
            return DateTime.Parse(DataProvider.Instance.ExecuteScalar("Select Ngaydat From HD_Nhaphang Where Sohd = @Sohd", new object[] { maNH }).ToString());
        }

        public DataTable timKiemHDNH(String tukhoa)
        {
            return DataProvider.Instance.ExecuteQueryOneParameter("Select Sohd, Ngaydat, Tongtien, Trangthai FROM HD_Nhaphang where (Sohd LIKE '%' + @tukhoa + '%' or convert(nvarchar, Ngaydat, 103) LIKE '%' + @tukhoa + '%'  or FORMAT(TongTien, 'N0') LIKE '%' + @tukhoa + '%' or Trangthai LIKE '%' + @tukhoa + '%')", new object[] { tukhoa });
        }

        // -------------------------------- Nhà Cung cấp ------------------------------------

        //Hàm thêm nhà cung cấp
        public bool AddNCC(string tenNCC, string diaChi, string maSoThue, string sdt)
        {

            // Gọi stored procedure và lấy mã nhà cung cấp
            string query = "EXEC themMaNhacungcap @TenNCC , @Diachi , @Masothue , @Sodienthoai ";
            object[] parameters =
            {
            tenNCC,
            diaChi,
            maSoThue,
            sdt
            };

            object result = DataProvider.Instance.ExecuteScalar(query, parameters);
            if (result == null) return false; // Thêm thất bại

            string maNCCMoi = result.ToString(); // Lấy mã NCC vừa được tạo tự động từ procedure

            return true;
        }

        //Lấy danh sách NCC từ database
        public DataTable GetNCCList()
        {
            string query = @"
        SELECT ncc.MaNCC, ncc.TenNCC, ncc.Diachi, ncc.Masothue, ncc.Sodienthoai
        FROM Nhacungcap ncc
        Where ncc.Xoa = 1";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        //Update Nha cung cap
        public bool UpdateNCC(string maNCC, string tenNCC, string diaChi, string maSoThue, string sdt)
        {
            string query = "UPDATE Nhacungcap SET TenNCC = @TenNCC , Diachi = @Diachi , Masothue = @Masothue, Sodienthoai = @Sodienthoai WHERE MaNCC = @MaNCC ";

            object[] parameters = { maNCC, diaChi, maSoThue, sdt, maNCC };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        //Delete Nha cung cap
        public bool DeleteNCC(string maNCC)
        {
            string query = "UPDATE Nhacungcap SET Xoa = 0 WHERE MaNCC = @MaNCC ";
            object[] parameters = { maNCC };

            return DataProvider.Instance.ExecuteNonQuery(query, parameters) > 0;
        }
        public DataTable timKiemDH(String tukhoa)
        {
            return DataProvider.Instance.ExecuteQueryOneParameter("SELECT h.Mahanghoa, h.Tenhanghoa, h.Tiennhap, h.Tendanhmuc, h.Tienban, h.ImageData, h.Soluong, h.Uudai, n.MaNCC, h.THSD FROM Hanghoa h JOIN Nhacungcap n ON h.MaNCC = n.MaNCC WHERE h.Xoa = 1 AND ( h.Mahanghoa    LIKE '%' + @tukhoa + '%' OR h.Tenhanghoa  LIKE '%' +'%' OR n.MaNCC LIKE '%' + '%' OR h.Tiennhap LIKE '%' + @tukhoa + '%'OR h.Tendanhmuc  LIKE '%' + @tukhoa + '%')", new object[] { tukhoa });
        }

    }
}
