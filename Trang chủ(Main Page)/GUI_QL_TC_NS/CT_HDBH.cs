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

namespace Trang_chu_Main_Page_.GUI_QL_TC_NS
{
    public partial class CT_HDBH : Form
    {
        private readonly BLL_QuanlyTCNS bLL_QuanlyTCNS = new BLL_QuanlyTCNS();

        public CT_HDBH()
        {
            InitializeComponent();
        }

        private void CT_HDBH_Load(object sender, EventArgs e)
        {
          


     
        }
        public void LoadCTHDBH(string maHoaDon)
        {

            // Gọi BLL để lấy dữ liệu chi tiết hóa đơn
            DataTable dataTable = bLL_QuanlyTCNS.xemChiTietHDBH(maHoaDon);
            dtg_HistoryMH.DataSource = dataTable;
            dtg_HistoryMH.Columns[0].HeaderText = "Mã Hóa Đơn";
            dtg_HistoryMH.Columns[1].HeaderText = "Mã Hàng Hóa";
            dtg_HistoryMH.Columns[2].HeaderText = "Tên Hàng Hóa";
            dtg_HistoryMH.Columns[3].HeaderText = "Số Lượng";
            dtg_HistoryMH.Columns[4].HeaderText = "Tổng Tiền";
            dtg_HistoryMH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtg_HistoryMH.ColumnHeadersHeight = 30; // Đặt chiều cao header là 30 pixel
            foreach (DataGridViewColumn column in dtg_HistoryMH.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.False;
                column.ReadOnly = true; // Khóa tất cả cột

            }
            // Kiểm tra nếu có dữ liệu thì hiển thị



        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtg_CTDH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

