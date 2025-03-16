using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using Microsoft.Identity.Client;

namespace DAL
{
    public class DAL_QuanlyTCNS
    {

        //Xem danh sách nhân viên
        public DataTable xemDSKH()
        {
            return DataProvider.Instance.ExecuteQuery("Select Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang From Khachhang where Xoa = 1");
        }


        //Xem danh sách khách hàng
        public DataTable xemDSNV()
        {
            return DataProvider.Instance.ExecuteQuery("Select Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai From Nhanvien where Xoa = 1");
        }

        //Thêm khách hàng
        public bool themKH(DTO_Khachhang kh)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery(
                    "exec themKH @Sodienthoai , @Hoten , @Diachi , @Gioitinh", new object[] { kh.soDienThoai, kh.hoTen, kh.diaChi, kh.gioiTinh }
                );
                if (line != 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Khách hàng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        //Xóa khách hàng
        public bool xoaKH(String soDienThoai)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery("Update Khachhang set Xoa = 0 Where Sodienthoai = @Sodienthoai", new object[] { soDienThoai });
                if (line != 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool suaKH(DTO_Khachhang kh)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery(
                    "Update Khachhang set Hoten = @Hoten , Diachi = @Diachi , Diemthuong = @Diemthuong , Gioitinh = @Gioitinh , Hang = @Hang Where Sodienthoai = @Sodienthoai", new object[] { kh.hoTen, kh.diaChi, kh.diemThuong, kh.gioiTinh, kh.hang, kh.soDienThoai }
                );
                if (line != 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Khách hàng không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        //Tìm kiếm khách hàng
        public DataTable timKiemKH(String tukhoa)
        {
            return DataProvider.Instance.ExecuteQueryOneParameter("Select Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang From Khachhang where (Sodienthoai LIKE '%' + @tukhoa + '%' or Hoten LIKE '%' + @tukhoa + '%') and Xoa = 1", new object[] { tukhoa});
        }


        //Sắp xếp khách hàng
        public DataTable sapXepKH(int chonIndex)
        {
            switch(chonIndex)
            {
                case 0:
                    return DataProvider.Instance.ExecuteQuery("Select Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang From Khachhang where Xoa = 1 order by Diemthuong asc");
                case 1:
                    return DataProvider.Instance.ExecuteQuery("Select Sodienthoai, Hoten, Diachi, Diemthuong, Gioitinh, Hang From Khachhang where Xoa = 1 order by Diemthuong desc");
            }
            return null;
        }

        public DataTable timKiemNV(String tukhoa)
        {
            return DataProvider.Instance.ExecuteQueryOneParameter("Select Manhanvien, Hoten, CCCD, Ngaysinh, Gioitinh, Diachi, Sodienthoai From Nhanvien where Xoa = 1 AND (Manhanvien LIKE + '%' + @tukhoa + '%' or Hoten LIKE + '%' + @tukhoa + '%') or CCCD LIKE + '%' + @tukhoa + '%' or Sodienthoai LIKE + '%' + @tukhoa + '%'", new object[] {tukhoa});
        }

        public Dictionary<String,DTO_Calam> toMauThoiKhoaBieu(String startDate, String endDate)
        {
            //Ghép hai bảng lấy dữ liệu ca làm và bắt buộc
            DataTable dt = DataProvider.Instance.ExecuteQuery("Select * From Calam cl join Batbuoc bb on cl.Macalam = bb.Macalam" +
                " Where ThoigianBD > @startDate AND ThoigianKT < @endDate", new object[] {startDate, endDate });

            //Tạo danh sách để lưu dữ liệu Key là mã ca làm, Value là DTO_Calam
            Dictionary<String,DTO_Calam> list = new Dictionary<String, DTO_Calam>();
            foreach (DataRow row in dt.Rows)
            {
                //Kiểm tra xem mã ca làm đã tồn tại trong danh sách chưa
                if (list.ContainsKey(row[0].ToString()))
                {
                    //Nếu đã tồn tại thì thêm nhân viên vào danh sách
                    list[row[0].ToString()].PC_Nhanvien.Add(row[6].ToString());                }
                else
                {
                    //Nếu chưa tồn tại thì thêm vào danh sách
                    list[row[0].ToString()] = new DTO_Calam(
                    row[0].ToString(),
                    row[1].ToString(),
                    Convert.ToDateTime(row[2]),
                    Convert.ToDateTime(row[3]),
                    int.Parse(row[4].ToString()),
                    new List<string> { row[6].ToString() } // Tạo danh sách và thêm phần tử
                );
                }
                
            }
            return list;
        }

        public DataTable xemDSNVLamViec()
        {
            return DataProvider.Instance.ExecuteQuery("Select Manhanvien, Hoten From Nhanvien where Xoa = 1");
        }

        public bool themCaLam(DTO_Calam caLam)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery(
                    "exec themMacaLam @TenCaLam , @TgBatDau , @TgKetThuc , @SoLuongNhanVien", new object[] {caLam.tenCaLam, caLam.tgBatDau.ToString("yyyy/MM/dd HH:mm:ss"), caLam.tgKetThuc.ToString("yyyy/MM/dd HH:mm:ss"), caLam.soLuongNhanVien }
                );
                if (line != 0)
                {
                    object maCalamObject = DataProvider.Instance.ExecuteScalar("Select Macalam From Calam Order by cast(substring(Macalam, 3, len(Macalam) - 2) as INT) desc");
                    for (int i = 0; i < caLam.PC_Nhanvien.Count; i++)
                    {
                        int lineNext = DataProvider.Instance.ExecuteNonQuery(
                            "Insert into Batbuoc values( @Macalam , @Manhanvien )", new object[] { maCalamObject.ToString(), caLam.PC_Nhanvien[i] }
                        );
                        if (lineNext == 0)
                        {
                            return false;
                        }
                    }

                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Ca làm đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public bool xoaCaLam(String maCaLam)
        {
            try
            {
                int line = DataProvider.Instance.ExecuteNonQuery("Delete from Batbuoc where Macalam = @Macalam ", new object[] { maCaLam });
                if (line != 0)
                {
                    int lineNext = DataProvider.Instance.ExecuteNonQuery("Delete from Calam where Macalam = @Macalam", new object[] { maCaLam });
                    if (lineNext != 0)
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
    }
}
