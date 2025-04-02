using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                dgvCTHH.Columns["NgaySanXuat"].HeaderText = "Lô Hàng";
                dgvCTHH.Columns["Hansudung"].HeaderText = "Hạn Sử Dụng";
                dgvCTHH.Columns["Soluongnhan"].HeaderText = "Số Lượng Hàng Hóa";
                dgvCTHH.Columns["Mahanghoa"].HeaderText = "Mã Hàng Hóa";

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
    }
}
