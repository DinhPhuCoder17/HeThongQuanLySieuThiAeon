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

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class CTHH : Form
    {
        public DataTable DataCTHH { get; set; }
        public CTHH()
        {
            InitializeComponent();
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
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
