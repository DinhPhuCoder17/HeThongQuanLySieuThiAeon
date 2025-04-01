using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            dtg_Bill.Columns[0].HeaderText = "Mã Hóa Đơn";
            dtg_Bill.Columns[1].HeaderText = "Thời Gian Bán";
            dtg_Bill.Columns[2].HeaderText = "Mã Nhân Viên";
            dtg_Bill.Columns[3].HeaderText = "Số Điện Thoại";
            dtg_Bill.Columns[4].HeaderText = "Thành Tiền";


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

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (bLL_QuanlyTCNS.XoaHoaDon(maHoaDon))
                    {
                        MessageBox.Show("Xóa hóa đơn thành công!");
                        DataTable dataTable = bLL_QuanlyTCNS.xemDSHD();
                        dtg_Bill.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại, vui lòng thử lại.");
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!");
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
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng thực hiện nếu sai điều kiện
            }
            DataTable dt = bLL_QuanlyTCNS.locHoaDon(fromDate, toDate);

            dtg_Bill.DataSource = dt; // Hiển thị trên DataGridView
        }
    }
}
