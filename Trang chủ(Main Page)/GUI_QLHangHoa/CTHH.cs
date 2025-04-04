using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trang_chủ_Main_Page_;

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class CTHH : Form
    {
        // Lưu danh sách các hàng hóa từ XemDSTonKho() vào biến toàn cục
        List<DTO_Hanghoa> dsHangHoa = BLLQuanLyKho.Instance.XemDSTonKho();

        public DataTable DataCTHH { get; set; }
        public CTHH()
        {
            InitializeComponent();
            dgvCTHH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

        }

        private void CTHH_Load(object sender, EventArgs e)
        {
            if (DataCTHH != null)
            {
                dgvCTHH.DataSource = DataCTHH;

                // Xác định các chuỗi hiển thị dựa trên culture hiện tại
                string headerNgaySanXuat, headerHansudung, headerSoluongnhan, headerMahanghoa;

                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    headerNgaySanXuat = "Batch";
                    headerHansudung = "Expiry Date";
                    headerSoluongnhan = "Product Quantity";
                    headerMahanghoa = "Product Code";
                }
                else  // Mặc định là tiếng Việt
                {
                    headerNgaySanXuat = "Lô Hàng";
                    headerHansudung = "Hạn Sử Dụng";
                    headerSoluongnhan = "Số Lượng Hàng Hóa";
                    headerMahanghoa = "Mã Hàng Hóa";
                }

                // Thiết lập HeaderText cho các cột
                dgvCTHH.Columns["NgaySanXuat"].HeaderText = headerNgaySanXuat;
                dgvCTHH.Columns["Hansudung"].HeaderText = headerHansudung;
                dgvCTHH.Columns["Soluongnhan"].HeaderText = headerSoluongnhan;
                dgvCTHH.Columns["Mahanghoa"].HeaderText = headerMahanghoa;

                // Đặt vị trí hiển thị của các cột
                dgvCTHH.Columns["NgaySanXuat"].DisplayIndex = 0;
                dgvCTHH.Columns["Mahanghoa"].DisplayIndex = 1;
                dgvCTHH.Columns["Hansudung"].DisplayIndex = 2;
                dgvCTHH.Columns["Soluongnhan"].DisplayIndex = 3;
            }
            HighlightHansudungInCTHH();
        }

        private int GetTHSDFromList(string maHangHoa)
        {
            var hangHoa = dsHangHoa.FirstOrDefault(hh => hh.MaHangHoa == maHangHoa);
            if (hangHoa != null)
            {
                return hangHoa.THSD;
            }
            return -1;  
        }
        private void HighlightHansudungInCTHH()
        {
            foreach (DataGridViewRow row in dgvCTHH.Rows)
            {
                string maHangHoa = row.Cells["Mahanghoa"].Value.ToString();
                int thsd = GetTHSDFromList(maHangHoa);
                if (thsd != -1)
                {
                    DateTime hansudungDate = Convert.ToDateTime(row.Cells["Hansudung"].Value);
                    int remainingDays = (hansudungDate - DateTime.Now).Days;
                    if (thsd > 30 && remainingDays < (0.15 * thsd))
                    {
                        row.Cells["Hansudung"].Style.BackColor = Color.Yellow;
                    }
                }
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (DataCTHH != null)
            {
                string keyword = txtSearchCTHH.Text.Trim();
                DataView dv = new DataView(DataCTHH);
                string filter = "";

                foreach (DataColumn col in DataCTHH.Columns)
                {
                    if (col.DataType == typeof(string))
                    {
                        if (filter.Length > 0) filter += " OR ";
                        filter += $"{col.ColumnName} LIKE '%{keyword}%'";
                    }
                    else if (col.DataType == typeof(int) || col.DataType == typeof(double) || col.DataType == typeof(decimal))
                    {
                        if (int.TryParse(keyword, out _)) 
                        {
                            if (filter.Length > 0) filter += " OR ";
                            filter += $"{col.ColumnName} = {keyword}";
                        }
                    }
                }

                dv.RowFilter = filter;
                dgvCTHH.DataSource = dv;
                HighlightHansudungInCTHH();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        // ---------------- Phần của Quang ----------------------
        bool menuExpand_3 = false;
        private void tm_InforChanges_Tick(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
                pn_infoChanges.Height += 20;
                if (pn_infoChanges.Height >= 280)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = true;
                }
            }
            else
            {
                pn_infoChanges.Height -= 20;
                if (pn_infoChanges.Height <= 0)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = false;
                }
            }
        }
        private void pb_Avata_Click(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        private void btn_Xacnhan_Click(object sender, EventArgs e)
        {
            string mk1 = tb_mk1.Text;
            string mk2 = tb_mk2.Text;
            if (string.IsNullOrEmpty(mk1))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(mk2))
            {
                MessageBox.Show("Vui lòng nhập xác nhận mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!mk1.Equals(mk2))
            {
                MessageBox.Show("Mật khẩu không trùng khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidPassword(mk1))
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (BLL_Nhanvien.Instance.UpdatePassword(Mainpage.CurrentUser.MaNhanvien, mk1))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
