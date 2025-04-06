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

namespace Trang_chu_Main_Page_.GUI_QL_TC_NS
{
    public partial class CT_LichSuHoaDon : Form
    {
        public CT_LichSuHoaDon()
        {
            InitializeComponent();
        }
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();

        private void dtg_HistoryMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void LoadCTLS(string maHoaDon)
        {
            // Gọi BLL để lấy dữ liệu chi tiết hóa đơn đã xóa
            DataTable dataTable = bLL_QuanlyTCNS.GetLichSuChiTietHoaDon(maHoaDon);

            // Kiểm tra ngôn ngữ hiện tại và thay đổi tiêu đề cột phù hợp
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                dtg_HistoryMH.DataSource = dataTable;
                // Tiếng Việt
                dtg_HistoryMH.Columns[0].HeaderText = "Mã Hóa Đơn";
                dtg_HistoryMH.Columns[1].HeaderText = "Mã Hàng Hóa";
                dtg_HistoryMH.Columns[2].HeaderText = "Tên Hàng Hóa";
                dtg_HistoryMH.Columns[3].HeaderText = "Số Lượng";
                dtg_HistoryMH.Columns[4].HeaderText = "Tổng Tiền";
               
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                dtg_HistoryMH.DataSource = dataTable;
                // Tiếng Anh
                dtg_HistoryMH.Columns[0].HeaderText = "Invoice Code";
                dtg_HistoryMH.Columns[1].HeaderText = "Product Code";
                dtg_HistoryMH.Columns[2].HeaderText = "Product Name";
                dtg_HistoryMH.Columns[3].HeaderText = "Quantity";
                dtg_HistoryMH.Columns[4].HeaderText = "Total Amount";
             
            }

            // Cài đặt chiều cao của header
            dtg_HistoryMH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtg_HistoryMH.ColumnHeadersHeight = 30; // Đặt chiều cao header là 30 pixel

            // Cài đặt cho tất cả các cột
            foreach (DataGridViewColumn column in dtg_HistoryMH.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;  // Tắt chức năng sắp xếp cột
                column.Resizable = DataGridViewTriState.False; // Tắt tính năng thay đổi kích thước cột
                column.ReadOnly = true; // Khóa tất cả cột để không thể chỉnh sửa
            }

            // Cài đặt cho tất cả các hàng
            foreach (DataGridViewRow row in dtg_HistoryMH.Rows)
            {
                row.Resizable = DataGridViewTriState.False;  // Tắt tính năng thay đổi kích thước hàng
                row.ReadOnly = true; // Khóa tất cả các hàng
                row.Height = 30;  // Đặt chiều cao mỗi hàng là 30 pixel
            }

            // Kiểm tra nếu có dữ liệu thì hiển thị
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có chi tiết lịch sử hóa đơn.");
            }
        }

        private void CT_LichSuHoaDon_Load(object sender, EventArgs e)
        {
            if (dtg_HistoryMH.Columns.Count == 0)
            {
                // Tạo các cột cho DataGridView
                dtg_HistoryMH.Columns.Add("InvoiceCode", "Mã Hóa Đơn");
                dtg_HistoryMH.Columns.Add("ProductCode", "Mã Hàng Hóa");
                dtg_HistoryMH.Columns.Add("ProductName", "Tên Hàng Hóa");
                dtg_HistoryMH.Columns.Add("Quantity", "Số Lượng");
                dtg_HistoryMH.Columns.Add("TotalAmount", "Tổng Tiền");
            }
        }
    }
}
