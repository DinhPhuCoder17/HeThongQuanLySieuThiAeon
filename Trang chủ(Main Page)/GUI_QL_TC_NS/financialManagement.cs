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
using System.Timers;
using System.Windows.Forms;
using BLL;
using Trang_chu_Main_Page_.GUI_QL_TC_NS;
using Microsoft.VisualBasic;
namespace Trang_chủ_Main_Page_
{
    public partial class financialManagement : Form
    {
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();
        public financialManagement()
        {
            InitializeComponent();
        }
        bool menuExpand = false;
    
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void financialManagement_Load(object sender, EventArgs e)
        {
            DataTable dataTable = bLL_QuanlyTCNS.xemDSHD();
            dtg_Bill.DataSource = dataTable;
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                // Tiếng Việt
                dtg_Bill.Columns[0].HeaderText = "Mã Hóa Đơn";
                dtg_Bill.Columns[1].HeaderText = "Thời Gian Bán";
                dtg_Bill.Columns[2].HeaderText = "Mã Nhân Viên";
                dtg_Bill.Columns[3].HeaderText = "Số Điện Thoại";
                dtg_Bill.Columns[4].HeaderText = "Thành Tiền";
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                // Tiếng Anh
                dtg_Bill.Columns[0].HeaderText = "Invoice Code";
                dtg_Bill.Columns[1].HeaderText = "Sale Time";
                dtg_Bill.Columns[2].HeaderText = "Employee Code";
                dtg_Bill.Columns[3].HeaderText = "Phone Number";
                dtg_Bill.Columns[4].HeaderText = "Total Amount";
            }


            foreach (DataGridViewColumn column in dtg_Bill.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dtg_Bill.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

        }
        
    
        private void btn_Bill_Cancel_Click(object sender, EventArgs e)
        {
            if (dtg_Bill.SelectedRows.Count > 0)
            {
                string maHoaDon = dtg_Bill.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();

                string confirmMessage = "";
                string titleMessage = "";

                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    confirmMessage = "Bạn có chắc chắn muốn xóa hóa đơn này?";
                    titleMessage = "Xác nhận xóa";
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    confirmMessage = "Are you sure you want to delete this invoice?";
                    titleMessage = "Delete Confirmation";
                }
               
                // Hiển thị MessageBox xác nhận
                DialogResult result = MessageBox.Show(confirmMessage, titleMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Gọi Form nhập lý do xóa
                   Lido reasonForm = new Lido();
                    if (reasonForm.ShowDialog() == DialogResult.OK)
                    {
                        string reason = reasonForm.Reason;

                        if (!string.IsNullOrEmpty(reason))
                        {
                            // Gọi Business Logic Layer để thực hiện xóa hóa đơn và lưu lý do
                            bool isDeleted = bLL_QuanlyTCNS.XoaHoaDon(maHoaDon, reason);

                            if (isDeleted)
                            {
                                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                                {
                                    MessageBox.Show("Xóa hóa đơn thành công!");
                                }
                                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                                {
                                    MessageBox.Show("Invoice deleted successfully!");
                                }

                                // Cập nhật lại danh sách hóa đơn
                                DataTable dataTable = bLL_QuanlyTCNS.xemDSHD();
                                dtg_Bill.DataSource = dataTable;
                            }
                            else
                            {
                                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                                {
                                    MessageBox.Show("Xóa thất bại, vui lòng thử lại.");
                                }
                                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                                {
                                    MessageBox.Show("Deletion failed, please try again.");
                                }
                            }
                        }
                        else
                        {
                            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                            {
                                MessageBox.Show("Vui lòng nhập lý do xóa!");
                            }
                            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                            {
                                MessageBox.Show("Please enter a reason for deletion!");
                            }
                        }
                    }
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please select an invoice to delete!");
                }
            }


        }

        private void dtg_Bill_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dtg_Bill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số dòng hợp lệ
            {
                // Lấy Mã Hóa Đơn từ cột đầu tiên
                string maHoaDon = dtg_Bill.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Khởi tạo form CT_HDBH và truyền Mã Hóa Đơn
                CT_HDBH chiTietHDBH = new CT_HDBH();
                chiTietHDBH.LoadCTHDBH(maHoaDon); // Gọi phương thức load dữ liệu
                chiTietHDBH.ShowDialog(); // Hiển thị form dưới dạng hộp thoại
                DataTable dataTable = bLL_QuanlyTCNS.xemDSHD();
                dtg_Bill.DataSource = dataTable;
            }
        }

        private void btn_Bill_FilterDate_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtp_Bill_Start.Value.Date;
            DateTime toDate = dtp_Bill_End.Value.Date.AddDays(1).AddSeconds(-1); // Lấy hết ngày đến 23:59:59
            if (fromDate > toDate)
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    // Tiếng Việt
                    MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    // Tiếng Anh
                    MessageBox.Show("The end date must be greater than or equal to the start date!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return; // Dừng thực hiện nếu sai điều kiện
            }
            DataTable dt = bLL_QuanlyTCNS.locHoaDon(fromDate, toDate);

            dtg_Bill.DataSource = dt; // Hiển thị trên DataGridView
        }

        private void txt_Bill_SearchBar_TextChanged(object sender, EventArgs e)
        {
            if (txt_Bill_SearchBar.Text == "")
            {
                financialManagement_Load(sender, e);
            }
            else
            {
                dtg_Bill.DataSource = bLL_QuanlyTCNS.timKiemHoaDon(txt_Bill_SearchBar.Text);
            }
        }

        // ---------------- Phần của Quang ----------------------
        bool menuExpand_3 = false;
       
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private void pb_Avata_Click_1(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        private void tm_InforChanges_Tick_1(object sender, EventArgs e)
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

        private void btn_History_Bill_Click(object sender, EventArgs e)
        {
            // Lấy danh sách lịch sử hóa đơn
            DataTable dataTable = bLL_QuanlyTCNS.GetLichSuHoaDon(); // Gọi phương thức BLL để lấy dữ liệu lịch sử hóa đơn
            dgvLichSuHoaDon.DataSource = dataTable;  // Hiển thị dữ liệu vào DataGridView

            // Điều chỉnh tiêu đề cột dựa trên ngôn ngữ
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                // Tiếng Việt
                dgvLichSuHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
                dgvLichSuHoaDon.Columns[1].HeaderText = "Thời Gian Xóa";
                dgvLichSuHoaDon.Columns[2].HeaderText = "Mã Nhân Viên";
                dgvLichSuHoaDon.Columns[3].HeaderText = "Số Điện Thoại";
                dgvLichSuHoaDon.Columns[4].HeaderText = "Lý Do Xóa";
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                // Tiếng Anh
                dgvLichSuHoaDon.Columns[0].HeaderText = "Invoice Code";
                dgvLichSuHoaDon.Columns[1].HeaderText = "Deletion Time";
                dgvLichSuHoaDon.Columns[2].HeaderText = "Employee Code";
                dgvLichSuHoaDon.Columns[3].HeaderText = "Phone Number";
                dgvLichSuHoaDon.Columns[4].HeaderText = "Reason for Deletion";
            }

            // Tắt tính năng sắp xếp cột
            foreach (DataGridViewColumn column in dgvLichSuHoaDon.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Tắt tính năng thay đổi kích thước cột
            foreach (DataGridViewColumn column in dgvLichSuHoaDon.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                guna2PanelH.Height += 50;
                if (guna2PanelH.Height >= 700)
                {
                    timer1.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                guna2PanelH.Height -= 50;
                if (guna2PanelH.Height <= 0)
                {
                    timer1.Stop();
                    menuExpand = false;
                }
            }
        }

        private void dgvLichSuHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
