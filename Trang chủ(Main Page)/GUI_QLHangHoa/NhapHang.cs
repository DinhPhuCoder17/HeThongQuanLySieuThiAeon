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

namespace Trang_chủ_Main_Page_
{
    public partial class NhapHang : Form
    {
        private readonly BLLQuanLyKho bll_QuanLyKho = new BLLQuanLyKho();
        public NhapHang()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NhapHang_Load(object sender, EventArgs e)
        {
            DataTable dataTable = bll_QuanLyKho.xemDSNH();
            dgvNhapHang.DataSource = dataTable;
            dgvNhapHang.Columns[0].HeaderText = "Mã đơn hàng";
            dgvNhapHang.Columns[1].HeaderText = "Ngày nhập";
            dgvNhapHang.Columns[2].HeaderText = "Tổng tiền";
            dgvNhapHang.Columns[3].HeaderText = "Trạng Thái";

            foreach (DataGridViewColumn column in dgvNhapHang.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvNhapHang.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

        }

        private void dgvNhapHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị cột đầu tiên (ví dụ: Mã đơn hàng)
                string maDH = dgvNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Tạo form CTDH và truyền Mã đơn hàng
                CTDH cTDH = new CTDH();
                cTDH.UpdateMaDH(maDH); // Gọi phương thức cập nhật trên CTDH
                cTDH.ShowDialog(); // Hiển thị form chi tiết
            }
        }
    }
}
