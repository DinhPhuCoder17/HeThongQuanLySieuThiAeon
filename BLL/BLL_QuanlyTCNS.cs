using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;
using ExcelDataReader;

namespace BLL
{
    public class BLL_QuanlyTCNS
    {

        private readonly DAL_QuanlyTCNS dAL_QuanlyTCNS = new DAL_QuanlyTCNS();

      
        //Xem danh sách chấm công
        public DataTable xemDSCC()
        {
            return dAL_QuanlyTCNS.xemDSCC();
        }
        //Xem danh sách nhân viên
        public DataTable xemDSNV()
        {
            return dAL_QuanlyTCNS.xemDSNV();
        }

        //Xem danh sách khách hàng
        public DataTable xemDSKH()
        {
            return dAL_QuanlyTCNS.xemDSKH();
        }
        public bool ImportChamCongFromExcel(string filePath)
        {
        DAL_QuanlyTCNS dal = new DAL_QuanlyTCNS();
            try
            {
                // Đọc dữ liệu từ Excel
                List<DTO_Chamcong> danhSachChamCong = ReadExcelFile(filePath);

                // Duyệt danh sách và thêm vào CSDL
                foreach (var chamCong in danhSachChamCong)
                {
                    bool success = dal.ThemChamCong(chamCong);
                    if (!success) return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi import file Excel: " + ex.Message);
                return false;
            }
        }

        private List<DTO_Chamcong> ReadExcelFile(string filePath)
        {
            List<DTO_Chamcong> listChamCong = new List<DTO_Chamcong>();

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        DataTable dataTable = result.Tables[0];

                        for (int i = 1; i < dataTable.Rows.Count; i++) // Bỏ qua dòng tiêu đề
                        {
                            try
                            {
                                DTO_Chamcong chamCong = new DTO_Chamcong(
                                  null, // ID
                                  Convert.ToDateTime(dataTable.Rows[i][0]), // Thời gian chấm công
                                  TimeSpan.TryParse(dataTable.Rows[i][1].ToString().Trim(), out TimeSpan checkIn) ? checkIn : TimeSpan.Zero, // Check-in (mặc định 00:00 nếu lỗi)
                                  TimeSpan.TryParse(dataTable.Rows[i][2].ToString().Trim(), out TimeSpan checkOut) ? checkOut : TimeSpan.Zero, // Check-out (mặc định 00:00 nếu lỗi)
                                  0, // Số công (mặc định là 0)
                                  null, // Trạng thái (để null)
                                  dataTable.Rows[i][3]?.ToString(), // Mã ca làm
                                  dataTable.Rows[i][4]?.ToString() // Mã nhân viên
  );

                                listChamCong.Add(chamCong);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Lỗi đọc dữ liệu dòng {i + 1}: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi mở file Excel: " + ex.Message);
            }

            return listChamCong;
        }
        //THêm Chấm Công
        public bool ThemChamCong( DateTime thoiGianCN, TimeSpan CheckIn, TimeSpan checkOut, String maCaLam, String maNhanVien)
        {
            DTO_Chamcong cc = new DTO_Chamcong(null,thoiGianCN,CheckIn,checkOut,0,null,maCaLam,maNhanVien);
            if (dAL_QuanlyTCNS.ThemChamCong(cc))
            {
                return true;
            }
            return false;
        }
        //Thêm khách hàng
        public Boolean themKH(String Hoten, String Sodienthoai, String Diachi, String Gioitinh)
        {
            if(Regex.IsMatch(Hoten, "[0-9]"))
            {
                MessageBox.Show("Họ tên không được chứa số");
                return false;
            }
            if(Regex.IsMatch(Sodienthoai, "[^0-9]"))
            {
                MessageBox.Show("Số điện thoại không được chứa kí tự");
                return false;
            }
            if (Sodienthoai.Length != 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ");
                return false;
            }
            DTO_Khachhang kh = new DTO_Khachhang(Hoten, Sodienthoai, Gioitinh, Diachi, 0, null, null);
            if(dAL_QuanlyTCNS.themKH(kh))
            {
                return true;
            }
            return false;
        }

        //Xóa khách hàng
        public Boolean xoaKH(String soDienThoai)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                if (dAL_QuanlyTCNS.xoaKH(soDienThoai))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        //Sửa khách hàng
        public bool suaKH(String Sodienthoai, String Hoten, String Diachi, int Diemthuong, String Gioitinh, String hang)
        {
            if (Regex.IsMatch(Hoten, "[0-9]"))
            {
                MessageBox.Show("Họ tên không được chứa số");
                return false;
            }
            else if (Diemthuong < 0)
            {
                MessageBox.Show("Điểm thưởng không hợp lệ");
                return false;
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes) {
                    DTO_Khachhang kh = new DTO_Khachhang(Hoten, Sodienthoai, Gioitinh, Diachi, Diemthuong, hang, null);
                    if (dAL_QuanlyTCNS.suaKH(kh))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        //Tìm kiếm khách hàng
        public DataTable timKiemKH(String tukhoa)
        {
            return dAL_QuanlyTCNS.timKiemKH(tukhoa);
        }


        //Sắp xếp khách hàng
        public DataTable sapXepKH(int indexChon)
        {
            return dAL_QuanlyTCNS.sapXepKH(indexChon);
        }

        //Tìm kiếm nhân viên
        public DataTable timKiemNV(String tukhoa)
        {
            return dAL_QuanlyTCNS.timKiemNV(tukhoa);
        }

        //Tô màu thời khóa biểu
        public Dictionary<String, DTO_Calam> toMauThoiKhoaBieu(String startDate, String endDate)
        {
            return dAL_QuanlyTCNS.toMauThoiKhoaBieu(startDate, endDate);
        }

        //Xem nhân viên để xếp ca
        public DataTable xemDSNVLamViec()
        {
            return dAL_QuanlyTCNS.xemDSNVLamViec();
        }

        //Thêm ca làm
        public bool themCaLam(DTO_Calam caLam)
        {
            
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm ca làm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes) {
                    if (caLam.soLuongNhanVien < caLam.PC_Nhanvien.Count)
                    {
                        MessageBox.Show("Số lượng nhân viên không hợp lệ");
                        return false;
                    }
                    if (caLam.tgBatDau.TimeOfDay < TimeSpan.Parse("06:00"))
                    {
                        MessageBox.Show("Thời gian không hợp lệ - Trước 6:00");
                    return false;
                    }

                if (dAL_QuanlyTCNS.themCaLam(caLam))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            return false;
        }

        public bool xoaCaLam(String maCaLam)
        {
            if(dAL_QuanlyTCNS.xoaCaLam(maCaLam))
            {
                return true;
            }
            return false;
        }

        public List<String> listNhanVienHienTai(String Macalam)
        {
            DataTable dt = dAL_QuanlyTCNS.listNhanVienHienTai(Macalam);
            List<String> list = new List<String>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row[0].ToString());
            }
            return list;
        }

        public bool suaCaLam(DTO_Calam calam)
        {
            if(calam.PC_Nhanvien.Count <= calam.soLuongNhanVien)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa ca làm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if(dAL_QuanlyTCNS.suaCaLam(calam))
                    {
                        return true;
                    }
                }
            }
            MessageBox.Show("Số lượng nhân viên không hợp lệ");
            return false;
        }
        
    }
}
