using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Trang_chu_Main_Page_.GUI_QL_TC_NS;

namespace Trang_chủ_Main_Page_
{
    public partial class financialManagement : Form
    {
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();
        public financialManagement()
        {
            InitializeComponent();
        }

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
                DialogResult result = MessageBox.Show(confirmMessage, titleMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                if (result == DialogResult.Yes)
                {
                    if (bLL_QuanlyTCNS.XoaHoaDon(maHoaDon))
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Xóa hóa đơn thành công!");
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            MessageBox.Show("Invoice deleted successfully!");
                        }


                        DataTable dataTable = bLL_QuanlyTCNS.xemDSHD();
                        dtg_Bill.DataSource = dataTable;
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            // Tiếng Việt
                            MessageBox.Show("Xóa thất bại, vui lòng thử lại.");
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            // Tiếng Anh
                            MessageBox.Show("Deletion failed, please try again.");
                        }


                    }
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    // Tiếng Việt
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!");
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    // Tiếng Anh
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
    }
}
